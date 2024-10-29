using Counter.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Counter.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CountRequestDTO> Counters { get; set; }
        public DbSet<ReportRequestDTO> Reports { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-6TB0GCH;Database=CounterDb;User Id=sa;Password=1;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountRequestDTO>(entity =>
            {
                entity.ToTable("Counters")
                    .HasKey(c => c.UUID);
                
                entity.Property(c => c.UUID)
                    .HasColumnType("uniqueidentifier");

                entity.Property(c => c.SeriNumarasi)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnType("nvarchar(8)");

                entity.Property(c => c.OlcumZamani)
                    .HasColumnType("datetime2(0)");

                entity.Property(c => c.Akim)
                    .HasColumnType("decimal(8,2)");

                entity.Property(c => c.SonEndeks)
                    .HasColumnType("decimal(8,2)");

                entity.Property(c => c.Voltaj)
                    .HasColumnType("decimal(8,2)");
            });

            modelBuilder.Entity<ReportRequestDTO>(entity =>
            {
                entity.ToTable("Reports")
                    .HasKey(r => r.UUID);

                entity.Property(r => r.UUID)
                    .HasColumnType("uniqueidentifier");

                entity.Property(r => r.TalepTarihi)
                    .HasColumnType("datetime2(0)");

                entity.OwnsOne(r => r.Icerik, b =>
                {
                    b.WithOwner();
                    b.Property(i => i.OlcumZamani).HasColumnName("OlcumZamani").HasColumnType("datetime2(0)");
                    b.Property(i => i.Akim).HasColumnName("Akim").HasColumnType("decimal(8,2)");
                    b.Property(i => i.SonEndeks).HasColumnName("SonEndeks").HasColumnType("decimal(8,2)");
                    b.Property(i => i.Voltaj).HasColumnName("Voltaj").HasColumnType("decimal(8,2)");
                });
            });
        }

    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            //var db = new ApplicationDbContext(optionsBuilder.Options);
            //db.Database.EnsureCreated();
            return new ApplicationDbContext(optionsBuilder.Options);

        }
    }
}