using HomesApi.Models;
using Microsoft.AspNetCore.Components.Web;

namespace HomesApi.Dtos;

public class CreatePropertyDto
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string FullDescription { get; set; } = "";
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
}
