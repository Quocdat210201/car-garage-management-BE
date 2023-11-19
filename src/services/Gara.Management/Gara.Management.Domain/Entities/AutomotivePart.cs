using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class AutomotivePart : EntityBaseWithId
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public Guid CategoryId { get; set; }

        public AutomotivePartCategory Category { get; set; }

        public Guid AutomotivePartSupplierId { get; set; }

        public AutomotivePartSupplier AutomotivePartSupplier { get; set; }
    }
}
