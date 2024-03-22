namespace HomesApi.Dtos;

public class PropertyUpdateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? FullDescription { get; set; }
    public bool? IsAvailable { get; set; }
    public int? NumberOfBedrooms { get; set; }
    public bool? HasGarden { get; set; }
    public int? SquareMeeterage { get; set; }
    public int? MonthlyRent { get; set; }
    public string? Postcode { get; set; }
    public string? City { get; set; }
    public int? PropertyNumber { get; set; }
    public string? Street { get; set; }
}
