using Gara.Management.Domain.Entities;

namespace Gara.Management.Domain.Repositories
{
    public interface IAppointmentScheduleRepository
    {
        public Task<IEnumerable<AppointmentSchedule>> FindAppointmentScheduleByUserIdAsync(Guid userId);
    }
}