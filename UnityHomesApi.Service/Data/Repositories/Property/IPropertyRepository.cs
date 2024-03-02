using HomesApi.Models;

namespace HomesApi.Data.Repositories;

public interface IPropertyRepository
{
		Task<IEnumerable<Property>> GetAllPropertiesAsync();
		Task<IEnumerable<Property>> GetPropertiesByLocationAsync(string location);
		Task<Property> GetPropertyByIdAsync(long id);
		Task<Property> AddPropertyAsync(Property property);
		Task UpdatePropertyAsync(Property property);
		Task DeletePropertyAsync(long id);
		Task<bool> PropertyExistsAsync(long id);
}
