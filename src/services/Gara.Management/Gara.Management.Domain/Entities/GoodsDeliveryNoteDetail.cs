using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class GoodsDeliveryNoteDetail : EntityBaseWithId
    {
        public int ReceiveNumber { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public Guid AutomotivePartId { get; set; }

        public AutomotivePart AutomotivePart { get; set; }

        public Guid GoodsDeliveryNoteId { get; set; }

        public GoodsDeliveryNote GoodsDeliveryNote { get; set; }
    }
}