using HomesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HomesApi.Data.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly HomesDbContext _context;

    public PropertyRepository(HomesDbContext context)
    {
        _context = context;
    }

    public Task<Property> AddPropertyAsync(Property property)
    {
        throw new NotImplementedException();
    }

    public Task DeletePropertyAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        var query = _context.Properties.AsQueryable();
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetPropertiesByLocationAsync(string location)
    {
        IQueryable<Property> query = _context.Properties.Where(p => p.IsAvailable);

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(
                p =>
                    p.Postcode.Contains(location)
                    || p.City.Equals(location)
                    || p.Street.Equals(location)
            );
        }

        return await query.ToListAsync();
    }

    public Task<Property> GetPropertyByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PropertyExistsAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePropertyAsync(Property property)
    {
        throw new NotImplementedException();
    }
}
