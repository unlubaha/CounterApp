using Counter.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Counter.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CountDTO> Counters { get; set; }
        public DbSet<ReportDTO> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
