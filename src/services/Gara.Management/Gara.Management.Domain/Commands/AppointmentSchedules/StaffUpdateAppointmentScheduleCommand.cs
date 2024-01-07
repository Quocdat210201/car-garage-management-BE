using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Gara.Management.Domain.Commands.AppointmentSchedules
{
    public class AppointmentScheduleDetailRequest
    {
        public Guid? AutomotivePartId { get; set; }

        public int Quantity { get; set; }

        [Required]
        public Guid RepairServiceId { get; set; }

    }

    public class StaffUpdateAppointmentScheduleCommand : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }

        [Required]
        public List<AppointmentScheduleDetailRequest> RepairServiceUpdateRequests { get; set; }

        public int Status { get; set; }
    }

    public class StaffUpdateAppointmentScheduleHandler : IRequestHandler<StaffUpdateAppointmentScheduleCommand, ServiceResult>
    {
        private readonly IRepository<AppointmentSchedule> _repository;
        private readonly IRepository<AppointmentScheduleDetail> _appointmentScheduleDetailRepository;
        private readonly IRepository<AutomotivePart> _automotivePartRepository;
        private readonly IRepository<AutomotivePartInWarehouse> _automotivePartInWarehouseRepository;

        public StaffUpdateAppointmentScheduleHandler(IRepository<AppointmentSchedule> repository,
            IRepository<AutomotivePart> automotivePartRepository,
            IRepository<AutomotivePartInWarehouse> automotivePartInWarehouseRepository,
            IRepository<AppointmentScheduleDetail> appointmentScheduleDetailRepository)
        {
            _repository = repository;
            _automotivePartRepository = automotivePartRepository;
            _automotivePartInWarehouseRepository = automotivePartInWarehouseRepository;
            _appointmentScheduleDetailRepository = appointmentScheduleDetailRepository;
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

            foreach (var repairServiceUpdateRequest in request.RepairServiceUpdateRequests)
            {
                var repairService = await _automotivePartRepository.GetByIdAsync(repairServiceUpdateRequest.RepairServiceId);
                if (repairService == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages = new List<string> { $"Not found Repair Service by id {repairServiceUpdateRequest.RepairServiceId}" };
                    return result;
                }

                if (repairServiceUpdateRequest.AutomotivePartId == null) continue;

                var automotivePartInWarehouseList = await _automotivePartInWarehouseRepository
                .ListAsync(a => a.AutomotivePartId == repairServiceUpdateRequest.AutomotivePartId);

                var automotivePartInWarehouse = automotivePartInWarehouseList.FirstOrDefault();

                if (automotivePartInWarehouse == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages = new List<string> { $"Not found Automotive Part by id {repairServiceUpdateRequest.AutomotivePartId}" };
                    return result;
                }

                if (automotivePartInWarehouse.Quantity < repairServiceUpdateRequest.Quantity)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages = new List<string> { $"Automotive Part by id {repairServiceUpdateRequest.AutomotivePartId} is not enough" };
                    return result;
                }

                automotivePartInWarehouse.Quantity -= repairServiceUpdateRequest.Quantity;

                var appointmentScheduleDetail = _appointmentScheduleDetailRepository.ListAsync(a => a.AppointmentScheduleId == appointmentSchedule.Id && a.RepairServiceId == repairServiceUpdateRequest.RepairServiceId).Result.FirstOrDefault();

                if (appointmentScheduleDetail == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages = new List<string> { $"Not found Appointment Schedule Detail by appointmentScheduleId {appointmentSchedule.Id} & RepairServiceId {repairServiceUpdateRequest.RepairServiceId}" };
                    return result;
                }

                appointmentScheduleDetail.Quantity = automotivePartInWarehouse.Quantity;
                appointmentScheduleDetail.AutomotivePartInWarehouseId = automotivePartInWarehouse.Id;

                await _automotivePartInWarehouseRepository.UpdateAsync(automotivePartInWarehouse);

                await _appointmentScheduleDetailRepository.UpdateAsync(appointmentScheduleDetail);
            }

            appointmentSchedule.Status = request.Status;

            await _repository.UpdateAsync(appointmentSchedule);

            await _repository.SaveChangeAsync();

            result.Success(appointmentSchedule);

            return result;
        }
    }
}
