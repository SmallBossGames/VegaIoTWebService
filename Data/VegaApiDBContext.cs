using Microsoft.EntityFrameworkCore;

namespace VegaIoTApi.Data
{
    public class VegaApiDBContext : DbContext
    {
        public VegaApiDBContext(DbContextOptions<VegaApiDBContext> options)
            : base(options)
        { 
        }

        public VegaApiDBContext() { }

    }
}