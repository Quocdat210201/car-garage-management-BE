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

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<CarType> CarTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
