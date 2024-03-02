using HomesApi.Data.Repositories;
using HomesApi.Dots;
using HomesApi.Dtos;
using HomesApi.Helpers;
using HomesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IGeneralRepository _generalRepository;
    private readonly IAuthHelper _authHelper;

    public AuthController(
        IUserRepository userRepository,
        IGeneralRepository generalRepository,
        IAuthHelper authHelper
    )
    {
        _userRepository = userRepository;
        _generalRepository = generalRepository;
        _authHelper = authHelper;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(UserRegistrationDto userToRegister)
    {
        // If passwords do not match immediately return bad request
        if (userToRegister.Password != userToRegister.ConfirmPassword)
        {
            return BadRequest("Passwords do not match");
        }

        // Check that user does not already exist
        if (_userRepository.UserExistsFromEmail(userToRegister.Email))
        {
            return BadRequest("User already exists");
        }

        byte[] passwordSalt = _authHelper.CreatePasswordSalt();
        byte[] passwordHash = _authHelper.GetPasswordHash(userToRegister.Password, passwordSalt);
        UserAuth userAuth = new UserAuth
        {
            Email = userToRegister.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        User newUser = new User
        {
            UserName = userToRegister.UserName,
            Email = userToRegister.Email,
            FirstName = userToRegister.FirstName,
            LastName = userToRegister.LastName
        };

        _generalRepository.AddEntity<UserAuth>(userAuth);
        _generalRepository.AddEntity<User>(newUser);

        if (!_generalRepository.SaveChanges())
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save user");
        }

        return Ok("User registered");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(UserLoginDto userToLogin)
    {
        UserLoginConfirmationDto userForConfirmation = _userRepository.GetUserAuth(
            userToLogin.Email
        );

        if (userForConfirmation == null)
        {
            return Unauthorized("Invalid email or password");
        }

        byte[] passwordHash = _authHelper.GetPasswordHash(
            userToLogin.Password,
            userForConfirmation.PasswordSalt
        );

        if (!passwordHash.SequenceEqual(userForConfirmation.PasswordHash))
        {
            return Unauthorized("Invalid email or password");
        }

        try
        {
            long userId = _userRepository.GetUserIdFromEmail(userToLogin.Email);
            string token = _authHelper.CreateToken(userId, userToLogin.Email);
            return Ok(new Dictionary<string, string> { { "token", token } });
        }
        catch (Exception e)
        {
            return Unauthorized(e.Message);
        }
    }

    [HttpGet("RefreshToken")]
    public IActionResult RefreshToken()
    {
        string userEmail = User.FindFirst("email")?.Value ?? "";
        if (string.IsNullOrEmpty(userEmail))
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "Problem with access token"
            );
        }
        try
        {
            long userId = _userRepository.GetUserIdFromEmail(userEmail);

            return Ok(
                new Dictionary<string, string>
                {
                    { "token", _authHelper.CreateToken(userId, userEmail) }
                }
            );
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
