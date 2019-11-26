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

        DbSet<VegaTempDevice> TempDevices { get; set; } = null!;
        DbSet<VegaTempDeviceData> TempDeviceData { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VegaTempDevice>()
                .HasIndex(d => d.Eui);
        }
    }
}