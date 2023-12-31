using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class GoodsDeliveryNoteDetail : EntityBaseWithId
    {
        public int? ReceiveNumber { get; set; }

        public double? Price { get; set; }

        public double? Discount { get; set; }

        public Guid AutomotivePartInWarehouseId { get; set; }

        public AutomotivePartInWarehouse AutomotivePartInWarehouse { get; set; }

        public Guid GoodsDeliveryNoteId { get; set; }

        public GoodsDeliveryNote GoodsDeliveryNote { get; set; }
    }
}