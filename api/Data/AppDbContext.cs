using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(s => s.Id)
                .IsUnique();
        }

        public DbSet<User> Users { get; set; }
    }
}