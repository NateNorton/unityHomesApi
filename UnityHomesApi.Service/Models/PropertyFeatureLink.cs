namespace HomesApi.Models;

public class PropertyFeatureLink
{
    public long Id { get; set; }
    public long PropertyId { get; set; }
    public long PropertyFeatureId { get; set; }
    public Property? Property { get; set; }
    public PropertyFeature? PropertyFeature { get; set; }
}
