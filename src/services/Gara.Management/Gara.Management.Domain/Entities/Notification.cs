using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class Notification : EntityBaseWithId
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }

        public Bill Bill { get; set; }

        public Guid BillId { get; set; }

        public GaraApplicationUser User { get; set; }

        public Guid UserId { get; set; }

    }
}
