using Microsoft.EntityFrameworkCore;
using UMS.Models;

namespace UMS.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Login> Login { get; set; }

        public DbSet<UserDetails> UserDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>();

            modelBuilder.Entity<Login>();

            modelBuilder.Entity<UserDetails>();

            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Login>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<UserDetails>()
                .HasKey(x => x.UserId);

        }
    }
}
