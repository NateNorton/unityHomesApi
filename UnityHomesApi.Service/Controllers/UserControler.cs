using System.Security.Claims;
using HomesApi.Data.Repositories;
using HomesApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomesApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetUserDetails(long id)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return Unauthorized("Invalid user id");
        }

        try
        {
            var userDetails = await _userRepository.GetUserDetails(userId);
            if (userDetails == null)
            {
                return BadRequest("Failed to get user details");
            }
            return Ok(userDetails);
        }
        catch (Exception)
        {
            return StatusCode(500, "Failed to get user details");
        }
    }

    [HttpGet("favourites")]
    public async Task<IActionResult> GetFavouriteProperties()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return Unauthorized("Invalid user id");
        }

        try
        {
            var favouriteProperties = await _userRepository.GetUserFavouriteProperties(userId);
            if (favouriteProperties == null)
            {
                return BadRequest("Failed to get favourite properties");
            }
            return Ok(favouriteProperties);
        }
        catch (Exception)
        {
            return StatusCode(500, "Failed to get favourite properties");
        }
    }

    [HttpPost("favourites/{propertyId}")]
    public async Task<IActionResult> AddFavouriteProperty(long propertyId)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return Unauthorized("Invalid user id");
        }

        try
        {
            await _userRepository.AddFavouriteProperty(userId, propertyId);
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500, "Failed to add favourite property");
        }
    }

    [HttpDelete("favourites/{propertyId}")]
    public async Task<IActionResult> RemoveFavouriteProperty(long propertyId)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return Unauthorized("Invalid user id");
        }

        try
        {
            await _userRepository.RemoveFavouriteProperty(userId, propertyId);
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500, "Failed to remove favourite property");
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserDetails([FromBody] UserUpdateDto userUpdateDto)
    {
        Console.WriteLine("--------------------");
        Console.WriteLine("in the controller");
        Console.WriteLine(userUpdateDto.Email);
        Console.WriteLine(userUpdateDto.FirstName);
        Console.WriteLine(userUpdateDto.LastName);
        Console.WriteLine(userUpdateDto.PhoneNumber);
        Console.WriteLine(userUpdateDto.UserName);
        Console.WriteLine("--------------------");
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return Unauthorized("Invalid user id");
        }

        try
        {
            var updated = await _userRepository.UpdateUserDetails(userId, userUpdateDto);
            if (!updated)
            {
                return BadRequest("Failed to update user details");
            }
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500, "Failed to update user details");
        }
    }
}
