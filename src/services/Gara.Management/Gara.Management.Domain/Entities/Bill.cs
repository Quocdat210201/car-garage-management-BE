using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class Bill : EntityBaseWithId
    {
        public DateTime ReceiveCarDate { get; set; }

        public DateTime ReturnCarDate { get; set; }

        public double Total { get; set; }

        public int PaymentStatus { get; set; }

        public string Note { get; set; }

        public List<BillDetail> Details { get; set; }

        public Guid CarId { get; set; }

        public Car Car { get; set; }

        public Guid StaffId { get; set; }

        public GaraApplicationUser Staff { get; set; }

        public Guid CustomerId { get; set; }

        public GaraApplicationUser Customer { get; set; }
    }
}
