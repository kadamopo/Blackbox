using System;
using Microsoft.EntityFrameworkCore;

namespace ServiceBusQueueClient.Models
{
    public class DeviceContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceMessage> DeviceMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasKey(c => c.SerialNumber);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@CreateLocalServerConnectionString());
        }

        protected virtual string CreateLocalServerConnectionString()
        {
            string server = Environment.MachineName.ToUpper();
            string db = "DeviceMonitoringApiDB";
            string connectionString = $"Server={server};Database={db};Trusted_Connection=True;";

            return connectionString;
        }
    }
}
