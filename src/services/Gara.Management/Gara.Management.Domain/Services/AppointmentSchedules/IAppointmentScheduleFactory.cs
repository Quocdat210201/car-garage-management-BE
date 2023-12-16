namespace Gara.Management.Domain.Services.AppointmentSchedules
{
    public interface IAppointmentScheduleFactory
    {
        public IAppointmentScheduleService get(string role);
    }
}
