using HomesApi.Models;

namespace HomesApi.Data.Repositories;

public interface IPropertyRepository
{
    IQueryable<Property> GetAllProperties();
    IQueryable<Property> GetPropertiesByLocation(string location);
    Task<Property> GetPropertyByIdAsync(long id);
    Task<Property> AddProperty(Property property);
    Task UpdatePropertyAsync(Property property);
    Task<bool> DeletePropertyAsync(long id, long userId);
    bool PropertyExists(long id);
}
