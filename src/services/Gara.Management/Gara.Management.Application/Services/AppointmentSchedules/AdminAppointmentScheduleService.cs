using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;

namespace Gara.Management.Domain.Services.AppointmentSchedules
{
    public class AdminAppointmentScheduleService : IAppointmentScheduleService
    {
        private readonly IRepository<AppointmentSchedule> _repository;

        public AdminAppointmentScheduleService(IRepository<AppointmentSchedule> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AppointmentSchedule>> GetAppointmentSchedules(Guid userId)
        {
            var appointmentScheduleList = await _repository.GetWithIncludeAsync(null, 0, 0, aps => aps.Car, aps => aps.Car.Owner);

            return appointmentScheduleList;
        }
    }
}
