using Counter.Entities;
using Counter.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Counter.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CountDTO> Counters { get; set; }
        public DbSet<ReportDTO> Reports { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=127.0.0.1,1433;Database=CounterDb;User Id=sa;Password=1;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountDTO>().ToTable("Counters")
                .HasKey(c => c.UUID);

            modelBuilder.Entity<ReportDTO>().ToTable("Reports")
                .HasKey(r => r.UUID);

            modelBuilder.Entity<ReportDTO>()
                .OwnsOne(r => r.Icerik);
        }
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1,1433;Database=CounterDb;User Id=sa;Password=1;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

