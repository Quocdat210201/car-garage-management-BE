using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class Ward : EntityBaseWithId
    {
        public string Name { get; set; }

        public Guid DistrictId { get; set; }

        public District District { get; set; }
    }
}
