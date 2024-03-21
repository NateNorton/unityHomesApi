using System.Security.Claims;
using HomesApi.Data;
using HomesApi.Data.Repositories;
using HomesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PropertiesController : ControllerBase
{
    private readonly HomesDbContext _context;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;

    public PropertiesController(
        HomesDbContext context,
        IPropertyRepository propertyRepository,
        IUserRepository userRepository
    )
    {
        _context = context;
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
    }

    // Return all properties that are available by location
    // GET: api/Properties
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<Property>> GetProperties([FromQuery] string location = null)
    {
        if (!string.IsNullOrEmpty(location))
        {
            var properties = _propertyRepository.GetPropertiesByLocation(location);
            if (properties == null)
            {
                return NotFound();
            }

            return properties.ToList();
        }
        else
        {
            var properties = _propertyRepository.GetAllProperties();
            if (properties == null)
            {
                return NotFound();
            }
            return properties.ToList();
        }
    }

    // GET: api/Properties/5
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<Property>> GetProperty(long id)
    {
        var @property = await _context.Properties.FindAsync(id);

        if (@property == null)
        {
            return NotFound();
        }

        return @property;
    }

    // PUT: api/Properties/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProperty(long id, Property @property)
    {
        if (id != @property.Id)
        {
            return BadRequest();
        }

        _context.Entry(@property).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_propertyRepository.PropertyExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;d
            }
        }

        return NoContent();
    }

    // POST: api/Properties
    [HttpPost]
    public async Task<ActionResult<Property>> PostProperty([FromBody] Property property)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return BadRequest("Invalid user id");
        }

        property.UserId = userId;

        try
        {
            var addedProperty = await _propertyRepository.AddProperty(property);
            return Ok(addedProperty);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occured while creating the property.");
        }
    }

    // DELETE: api/Properties/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(long id)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return BadRequest("Invalid user id");
        }

        var success = await _propertyRepository.DeletePropertyAsync(id, userId);

        if (!success)
        {
            return NotFound("Property not found or you are not the owner");
        }

        return Ok("Property deleted");
    }
}
