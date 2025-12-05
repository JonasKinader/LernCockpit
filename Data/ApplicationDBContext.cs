using Microsoft.EntityFrameworkCore;

namespace LernCockpit.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Components.Models.User> Users { get; set; } = null!;
        // public DbSet<YourEntity> YourEntities { get; set; }
    }
}