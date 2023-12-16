using Gara.Management.Domain.Entities;
using Gara.Management.Domain.Repositories;
using Gara.Management.Domain.Services.AppointmentSchedules;

namespace Gara.Management.Domain.Services
{
    public class CustomerAppointmentScheduleService : IAppointmentScheduleService
    {
        private readonly IAppointmentScheduleRepository _appointmentScheduleRepository;

        public CustomerAppointmentScheduleService(IAppointmentScheduleRepository appointmentScheduleRepository)
        {
            _appointmentScheduleRepository = appointmentScheduleRepository;
        }

        public async Task<IEnumerable<AppointmentSchedule>> GetAppointmentSchedules(Guid userId)
        {

            var appointmentScheduleList = await _appointmentScheduleRepository.FindAppointmentScheduleByUserIdAsync(userId);

            return appointmentScheduleList;
        }
    }
}
