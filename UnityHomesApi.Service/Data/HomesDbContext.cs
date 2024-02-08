using HomesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HomesApi.Data
{
    public class HomesDbContext : DbContext
    {
        public HomesDbContext(DbContextOptions<HomesDbContext> options)
            : base(options) { }

        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<UserAuth> UserAuths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData.Initialize(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();

            modelBuilder.Entity<User>().Property(u => u.UserName).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<User>().Property(u => u.PhoneNumber).HasMaxLength(20);
        }
    }
}
