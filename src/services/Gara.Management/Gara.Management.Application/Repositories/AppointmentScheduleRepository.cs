using Gara.Management.Application.Data;
using Gara.Management.Domain.Entities;
using Gara.Management.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gara.Management.Application.Repositories
{
    public class AppointmentScheduleRepository : IAppointmentScheduleRepository
    {
        private readonly GaraManagementDBContent _garaManagementDBContent;

        public AppointmentScheduleRepository(GaraManagementDBContent garaManagementDBContent)
        {
            _garaManagementDBContent = garaManagementDBContent;
        }

        public Task<IEnumerable<AppointmentSchedule>> FindAppointmentScheduleByUserIdAsync(Guid userId)
        {
            var appointmentScheduleList = _garaManagementDBContent.AppointmentSchedules
                .Include(x => x.Car)
                .Where(x => x.Car.OwnerId == userId);

            return Task.FromResult(appointmentScheduleList.AsEnumerable());
        }
    }
}
