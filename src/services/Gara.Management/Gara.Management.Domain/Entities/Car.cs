using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class Car : EntityBaseWithId
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string RegistrationNumber { get; set; }

        public Guid CarTypeId { get; set; }

        public CarType CarType { get; set; }

        public Guid OwnerId { get; set; }

        public GaraApplicationUser Owner { get; set; }

        public List<Bill> Bills { get; set; }

        public List<AppointmentSchedule>? AppointmentSchedules { get; set; }
    }
}
