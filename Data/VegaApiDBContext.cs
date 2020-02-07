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

        public DbSet<VegaDevice> TempDevices { get; set; } = null!;
        public DbSet<VegaTempDeviceData> TempDeviceData { get; set; } = null!;
        //public DbSet<VegaMagnetDeviceData> MagnetDeviceDatas { get; set; } = null!;
        public DbSet<VegaImpulsDeviceData> ImpulsDeviceDatas { get; set; } = null!;
        public DbSet<VegaMoveDeviceData> MoveDeviceDatas { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new System.ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<VegaDevice>()
                .HasIndex(d => d.Eui);
        }
    }
}