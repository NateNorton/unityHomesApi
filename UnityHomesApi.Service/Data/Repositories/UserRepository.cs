using HomesApi.Dots;
using HomesApi.Models;

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
        return _context.Users.FirstOrDefault(u => u.Id == id);
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

    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }

    public void AddEntity<T>(T entity)
    {
        System.Console.WriteLine("Getting here");
        if (entity != null)
            _context.Add(entity);
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
}