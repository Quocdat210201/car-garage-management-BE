using Gara.Identity.Domain.Entities;

namespace Gara.Management.Domain.Entities
{
    public class GaraApplicationUser : ApplicationUser
    {
        // List of bills that this user is the customer
        public List<Bill>? Bills { get; set; }

        // List of FixedBills that this user is the staff
        public List<Bill>? FixedBills { get; set; }

        // List of WorkSchedules that this user is the staff
        public List<AppointmentSchedule>? AppointmentSchedules { get; set; }

        public List<Car>? Cars { get; set; }

        public Guid? WardId { get; set; }

        public Ward? Ward { get; set; }
    }
}
