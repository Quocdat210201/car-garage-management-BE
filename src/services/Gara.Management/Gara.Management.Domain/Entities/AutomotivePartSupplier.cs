using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class AutomotivePartSupplier : EntityBaseWithId
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public List<AutomotivePart> AutomotiveParts { get; set; }
    }
}
