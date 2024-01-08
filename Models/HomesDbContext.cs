using Microsoft.EntityFrameworkCore;

namespace HomesApi.Models
{
  public class HomesDbContext : DbContext
  {
    public HomesDbContext(DbContextOptions<HomesDbContext> options) : base(options)
    {
    }

    public DbSet<Property> Properties { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      SeedData.Initialize(modelBuilder);

      modelBuilder.Entity<User>()
        .HasIndex(u => u.userName)
        .IsUnique();

      modelBuilder.Entity<User>()
        .Property(u => u.userName)
        .IsRequired()
        .HasMaxLength(50);

      modelBuilder.Entity<User>()
        .Property(u => u.email)
        .HasMaxLength(100)
        .IsRequired();

      modelBuilder.Entity<User>()
        .Property(u => u.phoneNumber)
        .HasMaxLength(20);
    }
  }
}