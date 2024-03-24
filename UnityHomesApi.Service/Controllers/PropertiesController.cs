using System.Security.Claims;
using HomesApi.Data.Repositories;
using HomesApi.Dtos;
using HomesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;

    public PropertiesController(
        IPropertyRepository propertyRepository,
        IUserRepository userRepository
    )
    {
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
    }

    // Return all properties that are available by location
    // GET: api/Properties
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<Property>> GetProperties([FromQuery] string? location = null)
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

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<Property>> GetProperty(long id)
    {
        var property = await _propertyRepository.GetPropertyByIdAsync(id);

        if (property == null)
        {
            return NotFound();
        }

        return property;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProperty(long id, PropertyUpdateDto propertyDto)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return Unauthorized("Invalid user id");
        }

        // make sure the property is owned by the current user
        var existingProperty = await _propertyRepository.GetPropertyByIdAsync(id);
        if (existingProperty == null)
        {
            return NotFound("Property not found");
        }

        if (existingProperty.UserId != userId)
        {
            return Unauthorized("You are not the owner of this property");
        }

        var success = await _propertyRepository.UpdatePropertyAsync(id, propertyDto);

        if (!success)
        {
            return StatusCode(500, "An error occured while updating the property.");
        }

        return Ok("Property updated");
    }

    // POST: api/Properties
    [HttpPost]
    public async Task<ActionResult<Property>> PostProperty(
        [FromBody] CreatePropertyDto createPropertyDto
    )
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return BadRequest("Invalid user id");
        }

        try
        {
            var addedProperty = await _propertyRepository.AddProperty(createPropertyDto, userId);
            return Ok(addedProperty);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occured while creating the property.");
        }
    }

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
