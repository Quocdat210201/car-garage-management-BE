using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class AppointmentScheduleAutomotivePart : EntityBaseWithId
    {
        public string? Note { get; set; }

        public int Quantity { get; set; }

        public Guid AutomotivePartInWarehouseId { get; set; }

        public AutomotivePartInWarehouse AutomotivePartInWarehouse { get; set; }

        public Guid AppointmentScheduleId { get; set; }

        public AppointmentSchedule AppointmentSchedule { get; set; }
    }
}
