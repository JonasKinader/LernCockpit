using Microsoft.EntityFrameworkCore;
using LernCockpit.Components.Models;

namespace LernCockpit.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Folder> Folders { get; set; } = null!;
        public DbSet<LearningFile> LearningFiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Folder Selbstreferenz (Hauptthema -> Unterthemen)
            modelBuilder.Entity<Folder>()
                .HasOne(f => f.Hauptthema)
                .WithMany(f => f.Unterthema)
                .HasForeignKey(f => f.HauptthemaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Folder -> User
            modelBuilder.Entity<Folder>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // LearningFile -> Folder
            modelBuilder.Entity<LearningFile>()
                .HasOne(lf => lf.Folder)
                .WithMany(f => f.Files)
                .HasForeignKey(lf => lf.FolderId)
                .OnDelete(DeleteBehavior.Cascade);

            // LearningFile -> User
            modelBuilder.Entity<LearningFile>()
                .HasOne(lf => lf.User)
                .WithMany()
                .HasForeignKey(lf => lf.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}