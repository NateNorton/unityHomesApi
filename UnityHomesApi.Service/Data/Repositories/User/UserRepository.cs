using HomesApi.Dtos;
using HomesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HomesApi.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HomesDbContext _context;

    public UserRepository(HomesDbContext context)
    {
        _context = context;
    }

    public User GetUserById(long id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return user;
    }

    public async Task<UserDetailsDto> GetUserDetails(long userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return new UserDetailsDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName
        };
    }

    public bool UserExistsFromEmail(string Email)
    {
        return _context.Users.Any(u => u.Email == Email);
    }

    public bool AddUserAuth(UserAuth userAuth)
    {
        _context.UserAuths.Add(userAuth);
        _context.SaveChanges();
        return true;
    }

    public UserLoginConfirmationDto GetUserAuth(string email)
    {
        var userAuth = _context.UserAuths.FirstOrDefault(u => u.Email == email);
        if (userAuth == null)
        {
            throw new Exception("User not found");
        }

        return new UserLoginConfirmationDto
        {
            PasswordHash = userAuth.PasswordHash,
            PasswordSalt = userAuth.PasswordSalt
        };
    }

    public long GetUserIdFromEmail(string email)
    {
        // Get the user id from Users using email and return the ID
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return user.Id;
    }

    public string GetUsernameFromEmail(string email)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return user.UserName;
    }

    public async Task<List<Property>> GetUserFavouriteProperties(long userId)
    {
        var favouriteProperties = await _context
            .FavouriteProperties
            .Where(fp => fp.UserId == userId)
            .Select(fp => fp.Property)
            .ToListAsync();

        return favouriteProperties.Where(p => p != null).Cast<Property>().ToList();
    }

    public async Task AddFavouriteProperty(long userId, long propertyId)
    {
        var existinEntry = await _context
            .FavouriteProperties
            .FirstOrDefaultAsync(fp => fp.UserId == userId && fp.PropertyId == propertyId);

        if (existinEntry != null)
        {
            throw new Exception("Property already exists in favourites");
        }

        var newFavourite = new FavouriteProperty { UserId = userId, PropertyId = propertyId };

        _context.FavouriteProperties.Add(newFavourite);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFavouriteProperty(long userId, long propertyId)
    {
        var favorite = await _context
            .FavouriteProperties
            .FirstOrDefaultAsync(fp => fp.UserId == userId && fp.PropertyId == propertyId);

        if (favorite == null)
        {
            throw new Exception("Property not found in favourites");
        }

        _context.FavouriteProperties.Remove(favorite);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateUserDetails(long userId, UserUpdateDto userUpdateDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return false;
        }

        user.FirstName = userUpdateDto.FirstName ?? user.FirstName;
        user.LastName = userUpdateDto.LastName ?? user.LastName;
        user.PhoneNumber = userUpdateDto.PhoneNumber ?? user.PhoneNumber;
        user.UserName = userUpdateDto.UserName ?? user.UserName;
        user.Email = userUpdateDto.Email ?? user.Email;

        _context.Users.Update(user);

        var updated = await _context.SaveChangesAsync();

        return updated > 0;
    }
}
