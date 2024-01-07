using Microsoft.EntityFrameworkCore;
using Gara.Management.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Gara.Management.Application.Data
{
    public class GaraManagementDBContent : IdentityDbContext<GaraApplicationUser, GaraApplicationRole, Guid>
    {
        public GaraManagementDBContent(DbContextOptions<GaraManagementDBContent> options) : base(options)
        {

        }

        public virtual DbSet<AppointmentSchedule> AppointmentSchedules { get; set; }

        public virtual DbSet<AppointmentScheduleDetail> AppointmentScheduleDetails { get; set; }

        public virtual DbSet<AutomotivePart> AutomotiveParts { get; set; }

        public virtual DbSet<AutomotivePartInWarehouse> AutomotivePartInWarehouses { get; set; }

        public virtual DbSet<AutomotivePartSupplier> AutomotivePartSuppliers { get; set; }

        public virtual DbSet<Bill> Bills { get; set; }

        public virtual DbSet<CarBrand> CarBrands { get; set; }

        public virtual DbSet<GoodsDeliveryNote> GoodsDeliveryNotes { get; set; }

        public virtual DbSet<GoodsDeliveryNoteDetail> GoodsDeliveryNoteDetails { get; set; }

        public virtual DbSet<RepairService> RepairServices { get; set; }

        public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<CarType> CarTypes { get; set; }

        public virtual DbSet<District> Districts { get; set; }

        public virtual DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bill>()
                .HasOne(p => p.Staff)
                .WithMany(t => t.FixedBills)
                .HasForeignKey(m => m.StaffId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Bill>()
                .HasOne(p => p.Customer)
                .WithMany(t => t.Bills)
                .HasForeignKey(m => m.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Car>()
                .HasOne(s => s.Owner)
                .WithMany(g => g.Cars)
                .HasForeignKey(s => s.OwnerId);

            modelBuilder.Entity<AppointmentSchedule>()
                .HasOne(p => p.Staff)
                .WithMany(t => t.AppointmentSchedules)
                .HasForeignKey(m => m.StaffId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
