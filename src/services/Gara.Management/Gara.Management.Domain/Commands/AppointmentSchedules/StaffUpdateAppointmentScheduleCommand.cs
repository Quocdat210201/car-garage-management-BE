using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Commands.AppointmentSchedules
{
    public class StaffUpdateAppointmentScheduleCommand : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }

        public int Status { get; set; }
    }

    public class StaffUpdateAppointmentScheduleHandler : IRequestHandler<StaffUpdateAppointmentScheduleCommand, ServiceResult>
    {
        private readonly IRepository<AppointmentSchedule> _repository;

        public StaffUpdateAppointmentScheduleHandler(IRepository<AppointmentSchedule> repository)
        {
            _repository = repository;
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

            appointmentSchedule.Status = request.Status;

            await _repository.UpdateAsync(appointmentSchedule);

            await _repository.SaveChangeAsync();

            result.Success(appointmentSchedule);

            return result;
        }
    }
}
