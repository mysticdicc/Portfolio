using Microsoft.EntityFrameworkCore;
using PortfolioClassLibrary;

namespace Portfolio.Data
{
    public partial class PortfolioDatabase : DbContext
    {
        public PortfolioDatabase()
        {

        }

        public PortfolioDatabase(DbContextOptions<PortfolioDatabase> options)
            : base(options)
        {
        }

        public virtual DbSet<DevProjectPost> DevProjects { get; set; }
        public virtual DbSet<ItProjectPost> ItProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevProjectPost>(entity =>
            {
                entity.ToTable("dev_projects");

                entity.Property(e => e.ID);
                entity.Property(e => e.LastSubmit)
                    .IsRequired();
                entity.Property(e => e.Body)
                    .IsUnicode(false)
                    .IsRequired();
                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsRequired();
                entity.Property(e => e.Base64Images);
            });

            modelBuilder.Entity<ItProjectPost>(entity =>
            {
                entity.ToTable("it_projects");

                entity.Property(e => e.ID);
                entity.Property(e => e.LastSubmit)
                    .IsRequired();
                entity.Property(e => e.Body)
                    .IsUnicode(false)
                    .IsRequired();
                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsRequired();
                entity.Property(e => e.Base64Images);
            });
        }
    }
}
