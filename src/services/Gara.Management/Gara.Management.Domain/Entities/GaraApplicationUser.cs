using Gara.Identity.Domain.Entities;

namespace Gara.Management.Domain.Entities
{
    public class GaraApplicationUser : ApplicationUser
    {
        public List<AppointmentSchedule> Schedules { get; set; }

        public List<Car> Cars { get; set; }
    }
}
