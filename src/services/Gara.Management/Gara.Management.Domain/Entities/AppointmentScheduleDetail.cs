using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class AppointmentScheduleDetail : EntityBaseWithId
    {
        public string? Note { get; set; }

        public int Quantity { get; set; }

        public Guid? AutomotivePartInWarehouseId { get; set; }

        public AutomotivePartInWarehouse? AutomotivePartInWarehouse { get; set; }

        public Guid RepairServiceId { get; set; }

        public RepairService RepairService { get; set; }

        public Guid? BillId { get; set; }

        public Bill? Bill { get; set; }

        public Guid AppointmentScheduleId { get; set; }

        public AppointmentSchedule AppointmentSchedule { get; set; }
    }
}
