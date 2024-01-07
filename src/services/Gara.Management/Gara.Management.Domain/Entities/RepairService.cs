using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class RepairService : EntityBaseWithId
    {
        public string Name { get; set; }

        public string Thubmnail { get; set; }

        public string? Description { get; set; }

        public string? Detail { get; set; }

        public double Price { get; set; }

        public string? Note { get; set; }

        public List<AppointmentScheduleDetail> AppointmentScheduleDetails { get; set; }
    }
}
