using Gara.Management.Domain.Entities;

namespace Gara.Management.Domain.Services.AppointmentSchedules
{
    public interface IAppointmentScheduleService
    {
        public Task<IEnumerable<AppointmentSchedule>> GetAppointmentSchedules(Guid userId);
    }
}
