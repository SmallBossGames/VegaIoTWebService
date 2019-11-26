using Microsoft.EntityFrameworkCore;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Data
{
    public class VegaApiDBContext : DbContext
    {
        public VegaApiDBContext(DbContextOptions<VegaApiDBContext> options)
            : base(options)
        {
        }

        protected VegaApiDBContext()
        {
        }

        public DbSet<VegaTempDevice> TempDevices { get; set; } = null!;
        public DbSet<VegaTempDeviceData> TempDeviceData { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VegaTempDevice>()
                .HasIndex(d => d.Eui);
        }
    }
}