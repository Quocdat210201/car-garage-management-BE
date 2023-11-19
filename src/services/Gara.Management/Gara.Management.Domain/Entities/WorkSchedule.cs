using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class WorkSchedule : EntityBaseWithId
    {
        public Guid StaffId { get; set; }

        public GaraApplicationUser Staff { get; set; }
    }
}
