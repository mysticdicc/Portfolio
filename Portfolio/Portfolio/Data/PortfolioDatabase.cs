using Microsoft.EntityFrameworkCore;
using PortfolioClassLibrary;
using PortfolioClassLibrary.Classes.Images;
using PortfolioClassLibrary.Classes.DevProjects;
using PortfolioClassLibrary.Classes.ItProjects;
using PortfolioClassLibrary.Classes.Blog;

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
        public virtual DbSet<BlogPost> BlogPosts {  get; set; } 
        public virtual DbSet<Image> Images { get; set; }

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

                entity.HasMany(e => e.Images)
                    .WithOne(e => e.DevProjectPost)
                    .HasForeignKey(e => e.PostId)
                    .IsRequired(false);
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

                entity.HasMany(e => e.Images)
                    .WithOne(e => e.ItProjectPost)
                    .HasForeignKey(e => e.PostId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.ToTable("blog_posts");
                
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


                entity.HasMany(e => e.Images)
                    .WithOne(e => e.BlogPost)
                    .HasForeignKey(e => e.PostId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("images");

                entity.Property(e => e.Id);
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(e => e.LocalPath)
                    .HasMaxLength(150)
                    .IsRequired();
                entity.Property(e => e.RemotePath)
                    .HasMaxLength(150)
                    .IsRequired();
                entity.Property(e => e.FileExtension)
                    .HasMaxLength(25)
                    .IsRequired();
            });
        }
    }
}
