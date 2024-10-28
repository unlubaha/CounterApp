using Counter.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Counter.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CountDTO> Counters { get; set; }
        public DbSet<ReportDTO> Reports { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountDTO>().ToTable("Counters");
            modelBuilder.Entity<ReportDTO>().ToTable("Reports");
        }
    }
}
