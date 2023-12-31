namespace Gara.Management.Domain.Model
{
    public class GoodsDeliveryNoteDetailRequest
    {
        public int Quantity { get; set; }

        public double Price { get; set; }

        public double? Discount { get; set; }

        public Guid AutomotivePartId { get; set; }
    }
}
