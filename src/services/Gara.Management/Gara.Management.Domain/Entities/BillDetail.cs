using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class BillDetail : EntityBaseWithId
    {
        public string? Note { get; set; }

        public RepairService RepairService { get; set; }

        public Guid RepairServiceId { get; set; }

        public Guid BillId { get; set; }

        public Bill Bill { get; set; }
    }
}
