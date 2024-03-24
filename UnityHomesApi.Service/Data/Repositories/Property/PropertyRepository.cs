using HomesApi.Dtos;
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

    public async Task<Property> AddProperty(CreatePropertyDto createPropertyDto, long userID)
    {
        // create new Property using dto and user
        var property = new Property
        {
            Title = createPropertyDto.Title,
            Description = createPropertyDto.Description,
            FullDescription = createPropertyDto.FullDescription,
            IsAvailable = createPropertyDto.IsAvailable,
            NumberOfBedrooms = createPropertyDto.NumberOfBedrooms,
            HasGarden = createPropertyDto.HasGarden,
            SquareMeeterage = createPropertyDto.SquareMeeterage,
            MonthlyRent = createPropertyDto.MonthlyRent,
            Postcode = createPropertyDto.Postcode,
            City = createPropertyDto.City,
            PropertyNumber = createPropertyDto.PropertyNumber,
            Street = createPropertyDto.Street,
            PropertyTypeId = createPropertyDto.PropertyTypeId,
            UserId = userID
        };

        // add property to context
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
        // if saved ok return the property
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

    public async Task<Property?> GetPropertyByIdAsync(long propertyID)
    {
        return await _context
            .Properties
            .Include(p => p.PropertyType)
            .Select(
                p =>
                    new Property
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        FullDescription = p.FullDescription,
                        IsAvailable = p.IsAvailable,
                        NumberOfBedrooms = p.NumberOfBedrooms,
                        HasGarden = p.HasGarden,
                        SquareMeeterage = p.SquareMeeterage,
                        MonthlyRent = p.MonthlyRent,
                        Postcode = p.Postcode,
                        City = p.City,
                        PropertyNumber = p.PropertyNumber,
                        Street = p.Street,
                        PropertyTypeId = p.PropertyTypeId,
                        PropertyType = p.PropertyType,
                        DateAdded = p.DateAdded,
                        DateUpdated = p.DateUpdated,
                        UserId = p.UserId
                    }
            )
            .FirstOrDefaultAsync(p => p.Id == propertyID);
    }

    public bool PropertyExists(long id)
    {
        return _context.Properties.Any(e => e.Id == id);
    }

    public async Task<bool> UpdatePropertyAsync(long propertyId, PropertyUpdateDto updateDto)
    {
        var property = await _context.Properties.FindAsync(propertyId);

        if (property == null)
        {
            return false;
        }

        property.Title = updateDto.Title ?? property.Title;
        property.Description = updateDto.Description ?? property.Description;
        property.FullDescription = updateDto.FullDescription ?? property.FullDescription;
        property.IsAvailable = updateDto.IsAvailable ?? property.IsAvailable;
        property.NumberOfBedrooms = updateDto.NumberOfBedrooms ?? property.NumberOfBedrooms;
        property.HasGarden = updateDto.HasGarden ?? property.HasGarden;
        property.SquareMeeterage = updateDto.SquareMeeterage ?? property.SquareMeeterage;
        property.MonthlyRent = updateDto.MonthlyRent ?? property.MonthlyRent;
        property.Postcode = updateDto.Postcode ?? property.Postcode;
        property.City = updateDto.City ?? property.City;
        property.PropertyNumber = updateDto.PropertyNumber ?? property.PropertyNumber;
        property.Street = updateDto.Street ?? property.Street;

        _context.Properties.Update(property);

        await _context.SaveChangesAsync();

        return true;
    }
}
