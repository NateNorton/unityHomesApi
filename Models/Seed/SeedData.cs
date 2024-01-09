using HomesApi.Models;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
  public static void Initialize(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Property>().HasData(
        new Property
        {
          Id = 1,
          Title = "Cozy Cottage",
          Description = "A nice little cottage in the countryside.",
          IsAvailable = true,
          NumberOfBedrooms = 3,
          HasGarden = true,
          SquareMeeterage = 100,
          MonthlyRent = 300,
          Postcode = "AB12 3CD",
          City = "City 1",
          PropertyNumber = 1,
          Street = "Road 1"
        },
        new Property
        {
          Id = 2,
          Title = "Urban Apartment",
          Description = "Modern apartment in the city center.",
          IsAvailable = true,
          NumberOfBedrooms = 1,
          HasGarden = false,
          SquareMeeterage = 50,
          MonthlyRent = 200,
          Postcode = "BC34 5DE",
          City = "City 3",
          PropertyNumber = 2,
          Street = "Road 2"
        },
        new Property
        {
          Id = 3,
          Title = "Country House",
          Description = "A nice little cottage in the countryside.",
          IsAvailable = false,
          NumberOfBedrooms = 4,
          HasGarden = true,
          SquareMeeterage = 185,
          MonthlyRent = 600,
          Postcode = "XY98 7ZW",
          City = "City 3",
          PropertyNumber = 3,
          Street = "Road 3"
        }
    );
  }
}