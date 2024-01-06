using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Commands.AppointmentSchedules
{
    public class AppointmentAutomotivePart
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }
    }

    public class StaffUpdateAppointmentScheduleCommand : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }

        public List<AppointmentAutomotivePart> AutomotiveParts { get; set; }

        public int Status { get; set; }
    }

    public class StaffUpdateAppointmentScheduleHandler : IRequestHandler<StaffUpdateAppointmentScheduleCommand, ServiceResult>
    {
        private readonly IRepository<AppointmentSchedule> _repository;
        private readonly IRepository<AppointmentScheduleAutomotivePart> _appointmentScheduleAutomotivePartRepository;
        private readonly IRepository<AutomotivePart> _automotivePartRepository;
        private readonly IRepository<AutomotivePartInWarehouse> _automotivePartInWarehouseRepository;

        public StaffUpdateAppointmentScheduleHandler(IRepository<AppointmentSchedule> repository,
            IRepository<AutomotivePart> automotivePartRepository,
            IRepository<AutomotivePartInWarehouse> automotivePartInWarehouseRepository)
        {
            _repository = repository;
            _automotivePartRepository = automotivePartRepository;
            _automotivePartInWarehouseRepository = automotivePartInWarehouseRepository;
        }

        public async Task<ServiceResult> Handle(StaffUpdateAppointmentScheduleCommand request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();
            var appointmentSchedule = await _repository.GetByIdAsync(request.Id);

            if (appointmentSchedule == null)
            {
                result.ErrorMessages = new List<string> { $"Not found Appointment Schedule by id {request.Id}" };
                return result;
            }

            foreach (var automotivePart in request.AutomotiveParts)
            {
                var automotivePartInWarehouseList = await _automotivePartInWarehouseRepository
                    .ListAsync(a => a.AutomotivePartId == automotivePart.Id);

                var automotivePartInWarehouse = automotivePartInWarehouseList.FirstOrDefault();

                if (automotivePartInWarehouse == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages = new List<string> { $"Not found Automotive Part by id {automotivePart.Id}" };
                    return result;
                }

                if (automotivePartInWarehouse.Quantity < automotivePart.Quantity)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages = new List<string> { $"Automotive Part by id {automotivePart.Id} is not enough" };
                    return result;
                }

                automotivePartInWarehouse.Quantity -= automotivePart.Quantity;

                await _automotivePartInWarehouseRepository.UpdateAsync(automotivePartInWarehouse);

                var appointmentScheduleAutomotivePart = new AppointmentScheduleAutomotivePart
                {
                    AutomotivePartInWarehouseId = automotivePartInWarehouse.Id,
                    AppointmentScheduleId = appointmentSchedule.Id,
                    Quantity = automotivePart.Quantity
                };

                await _appointmentScheduleAutomotivePartRepository.AddAsync(appointmentScheduleAutomotivePart);

            }

            appointmentSchedule.Status = request.Status;

            await _repository.UpdateAsync(appointmentSchedule);

            await _repository.SaveChangeAsync();

            result.Success(appointmentSchedule);

            return result;
        }
    }
}
