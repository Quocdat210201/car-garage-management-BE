using Gara.Domain;
using Gara.Management.Domain.Enums;

namespace Gara.Management.Domain.Entities
{
    public class AppointmentSchedule : EntityBaseWithId
    {
        public DateTime AppointmentDate { get; set; }

        public string? Content { get; set; }

        public string? AdminWorkDetail { get; set; }

        public int Status { get; set; }

        public ReceiveCarAtEnum? ReceiveCarAt { get; set; }

        public string? ReceiveCarAddress { get; set; }

        public Guid CarId { get; set; }

        public Car Car { get; set; }

        public Guid? StaffId { get; set; }

        public GaraApplicationUser? Staff { get; set; }
    }
}
