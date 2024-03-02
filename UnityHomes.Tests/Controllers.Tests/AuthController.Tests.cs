using HomesApi.Controllers;
using HomesApi.Data.Repositories;
using HomesApi.Dots;
using HomesApi.Dtos;
using HomesApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSubstitute;

public class AuthControllerTests
{
    private readonly AuthController _authController;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IGeneralRepository _generalRepository = Substitute.For<IGeneralRepository>();
    private readonly IConfiguration _config = Substitute.For<IConfiguration>();
    private readonly IAuthHelper _authHelper = Substitute.For<IAuthHelper>();
    private readonly UserRegistrationDto _userToRegisterCorrect;

    public AuthControllerTests()
    {
        _authController = new AuthController(_userRepository, _generalRepository, _authHelper);
        _userToRegisterCorrect = new UserRegistrationDto
        {
            UserName = "testUser",
            Email = "test@test.com",
            Password = "password",
            ConfirmPassword = "password"
        };
    }

    [Fact]
    public void Register_WithNonMatchingPasswords_ReturnsBadRequest()
    {
        var registrationDto = new UserRegistrationDto
        {
            UserName = "testUser",
            Email = "test@test.com",
            Password = "password",
            ConfirmPassword = "password1"
        };
        var result = _authController.Register(registrationDto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Register_UserAlreadyExists_ReturnsBadRequest()
    {
        _userRepository.UserExistsFromEmail(Arg.Any<string>()).Returns(true);

        var result = _authController.Register(_userToRegisterCorrect);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Register_FailedToSaveUser_ReturnsInternalServerError()
    {
        _userRepository.UserExistsFromEmail(Arg.Any<string>()).Returns(false);
        _generalRepository.SaveChanges().Returns(false);

        var result = _authController.Register(_userToRegisterCorrect);

        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
    }

    [Fact]
    public void Register_Successful_ReturnsOK()
    {
        _userRepository.UserExistsFromEmail(Arg.Any<string>()).Returns(false);
        _generalRepository.SaveChanges().Returns(true);

        var result = _authController.Register(_userToRegisterCorrect);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("User registered", okResult.Value);
    }

    [Fact]
    public void Login_ValidCredentials_ReturnsOkWithToken()
    {
        // Arrange
        var userLoginDto = new UserLoginDto
        {
            Email = "user@example.com",
            Password = "correctPassword"
        };
        var userForConfirmation = new UserLoginConfirmationDto
        {
            PasswordSalt = new byte[128 / 8],
            PasswordHash = new byte[256 / 8]
        };
        _userRepository.GetUserAuth(userLoginDto.Email).Returns(userForConfirmation);
        _authHelper
            .GetPasswordHash(userLoginDto.Password, userForConfirmation.PasswordSalt)
            .Returns(userForConfirmation.PasswordHash);
        _userRepository.GetUserIdFromEmail(userLoginDto.Email).Returns(1L);
        _authHelper.CreateToken(Arg.Any<long>(), Arg.Any<string>()).Returns("ValidToken");

        var result = _authController.Login(userLoginDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public void Login_InvalidPassword_ReturnsUnauthorized()
    {
        var userLoginDto = new UserLoginDto { Email = "test@test.com", Password = "wrongPassword" };
        var userForConfirmation = new UserLoginConfirmationDto
        {
            PasswordHash = new byte[128 / 8],
            PasswordSalt = new byte[128 / 8]
        };
        _userRepository.GetUserAuth(userLoginDto.Email).Returns(userForConfirmation);
        _authHelper
            .GetPasswordHash(userLoginDto.Password, userForConfirmation.PasswordSalt)
            .Returns(
                new byte[128 / 8]
                    .Select(b => (byte)(b + 1))
                    .ToArray()
            );

        var result = _authController.Login(userLoginDto);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    [Fact]
    public void Login_UserNotFound_ReturnsUnauthorized()
    {
        var userLoginDto = new UserLoginDto
        {
            Email = "nonexistent@example.com",
            Password = "anyPassword"
        };

        var result = _authController.Login(userLoginDto);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }
}
