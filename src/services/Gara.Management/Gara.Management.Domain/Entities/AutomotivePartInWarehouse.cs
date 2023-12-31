using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class AutomotivePartInWarehouse : EntityBaseWithId
    {
        public int Quantity { get; set; }

        public double ReceivePrice { get; set; }

        public Guid AutomotivePartId { get; set; }

        public AutomotivePart AutomotivePart { get; set; }
    }
}
