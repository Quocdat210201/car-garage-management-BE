using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Gara.Management.Domain.Commands.AppointmentSchedules
{
    public class AdminAssignAppointmentScheduleToStaffCommand : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }

        [Required]
        public Guid StaffId { get; set; }

        public string? AdminWorkDetail { get; set; }
    }

    public class AdminAssignAppointmentScheduleToStaffHandler : IRequestHandler<AdminAssignAppointmentScheduleToStaffCommand, ServiceResult>
    {
        private readonly IRepository<AppointmentSchedule> _repository;

        public AdminAssignAppointmentScheduleToStaffHandler(IRepository<AppointmentSchedule> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(AdminAssignAppointmentScheduleToStaffCommand request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();
            var appointmentSchedule = await _repository.GetByIdAsync(request.Id);

            if (appointmentSchedule == null)
            {
                result.ErrorMessages = new List<string> { $"Not found Appointment Schedule by id {request.Id}" };
                return result;
            }

            appointmentSchedule.StaffId = request.StaffId;
            appointmentSchedule.AdminWorkDetail = request.AdminWorkDetail;

            await _repository.UpdateAsync(appointmentSchedule);

            await _repository.SaveChangeAsync();

            result.Success(appointmentSchedule);

            return result;
        }
    }
}
