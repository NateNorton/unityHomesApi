using Microsoft.EntityFrameworkCore;

namespace HomesApi.Models
{
  public class HomesDbContext : DbContext
  {
    public HomesDbContext(DbContextOptions<HomesDbContext> options) : base(options)
    {
    }

    public DbSet<Property> Properties { get; set; }
  }
}