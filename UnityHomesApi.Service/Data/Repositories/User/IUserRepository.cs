using HomesApi.Dots;
using HomesApi.Models;

namespace HomesApi.Data.Repositories;

public interface IUserRepository
{
    User GetUserById(long id);
    bool UserExistsFromEmail(string Email);
    UserLoginConfirmationDto GetUserAuth(string email);
    long GetUserIdFromEmail(string email);
}
