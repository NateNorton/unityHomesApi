using HomesApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HomesApi.Data.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly HomesDbContext _context;

    public PropertyRepository(HomesDbContext context)
    {
        _context = context;
    }

    public async Task<Property> AddProperty(Property property)
    {
        if (property == null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
        return property;
    }

    public async Task<bool> DeletePropertyAsync(long id, long userId)
    {
        var property = await _context
            .Properties
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        if (property == null)
        {
            return false;
        }

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();

        return true;
    }

    public IQueryable<Property> GetAllProperties()
    {
        var query = _context.Properties.AsQueryable();
        return query;
    }

    public IQueryable<Property> GetPropertiesByLocation(string location)
    {
        IQueryable<Property> query = _context.Properties.Where(p => p.IsAvailable);

        query = query.Where(
            p =>
                p.Postcode.Contains(location)
                || p.City.Equals(location)
                || p.Street.Equals(location)
        );

        return query;
    }

    public Task<Property> GetPropertyByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public bool PropertyExists(long id)
    {
        return _context.Properties.Any(e => e.Id == id);
    }

    public Task UpdatePropertyAsync(Property property)
    {
        throw new NotImplementedException();
    }
}
