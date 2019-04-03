using Microsoft.EntityFrameworkCore;

using Models;

namespace DeviceMonitoringAPI.Context
{
    public class DeviceContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }

        public DeviceContext(DbContextOptions<DeviceContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasKey(c => c.SerialNumber);
        }
    }
}
