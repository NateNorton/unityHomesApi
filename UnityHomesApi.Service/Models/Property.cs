namespace HomesApi.Models;

public class Property
{
    public long Id { get; set; }
    public string Title { get; set; } = "Default title";
    public string Description { get; set; } = "Default description";
    public string FullDescription { get; set; } = "Default full description";
    public bool IsAvailable { get; set; } = false;
    public int NumberOfBedrooms { get; set; } = 1;
    public bool HasGarden { get; set; } = false;
    public int SquareMeeterage { get; set; }
    public int MonthlyRent { get; set; }
    public string Postcode { get; set; } = "";
    public string City { get; set; } = "";
    public int PropertyNumber { get; set; }
    public string Street { get; set; } = "";
    public int PropertyTypeId { get; set; }
    public PropertyType? PropertyType { get; set; }
    public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.UtcNow;
    public long UserId { get; set; }
    public User? User { get; set; }
    public List<FavouriteProperty> FavouritedBy { get; set; } = new List<FavouriteProperty>();
}
