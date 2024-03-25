using HomesApi.Dtos;
using HomesApi.Models;

namespace HomesApi.Data.Repositories;

public interface IUserRepository
{
    User GetUserById(long id);
    bool UserExistsFromEmail(string Email);
    UserLoginConfirmationDto GetUserAuth(string email);
    long GetUserIdFromEmail(string email);

    string GetUsernameFromEmail(string email);
    Task<List<Property>> GetUserFavouriteProperties(long userId);
    Task AddFavouriteProperty(long userId, long propertyId);
    Task RemoveFavouriteProperty(long userId, long propertyId);
    Task<bool> UpdateUserDetails(long userId, UserUpdateDto userUpdateDto);
    Task<UserDetailsDto> GetUserDetails(long userId);
}
