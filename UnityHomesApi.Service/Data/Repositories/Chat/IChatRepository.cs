using HomesApi.Dtos;
using HomesApi.Models;

namespace HomesApi.Data.Repositories;

public interface IChatRepository
{
    Task<Message> AddMessageAsync(Message message);
    Task<Message?> GetMessageByIdAsync(long id);
    Task<bool> EditMessageAsync(long messageId, string newContent);
    Task<bool> DeleteMessageAsync(long messageId);
    Task<List<Message>> GetMessagesBetweenUsersAsync(long userId1, long userId2);
    Task<List<ChatParticipantDto>> GetChatParticipantsAsync(long userId);
}
