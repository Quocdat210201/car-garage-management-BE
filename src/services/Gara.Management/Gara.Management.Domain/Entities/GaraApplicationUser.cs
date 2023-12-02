using Gara.Identity.Domain.Entities;

namespace Gara.Management.Domain.Entities
{
    public class GaraApplicationUser : ApplicationUser
    {
        // List of bills that this user is the customer
        public List<Bill> Bills { get; set; }

        // List of FixedBills that this user is the staff
        public List<Bill> FixedBills { get; set; }

        public List<Car> Cars { get; set; }
    }
}
