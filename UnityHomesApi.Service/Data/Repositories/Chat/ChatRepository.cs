using HomesApi.Dtos;
using HomesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HomesApi.Data.Repositories;

public class ChatReposititory : IChatRepository
{
    private readonly HomesDbContext _context;

    public ChatReposititory(HomesDbContext context)
    {
        _context = context;
    }

    public async Task<Message> AddMessageAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<Message?> GetMessageByIdAsync(long id)
    {
        return await _context
            .Messages
            .Where(m => m.Id == id)
            .Select(
                m =>
                    new Message
                    {
                        Id = m.Id,
                        Content = m.Content,
                        DateSent = m.DateSent,
                        Edited = m.Edited,
                        SenderId = m.SenderId,
                        Sender = new User { Id = m.Sender.Id, UserName = m.Sender.UserName }
                    }
            )
            .FirstOrDefaultAsync();
    }

    public async Task<bool> EditMessageAsync(long messageId, string newContent)
    {
        var message = await _context.Messages.FindAsync(messageId);

        if (message == null)
        {
            return false;
        }

        message.Content = newContent;
        message.Edited = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteMessageAsync(long messageId)
    {
        var message = await _context.Messages.FindAsync(messageId);

        if (message == null)
        {
            return false;
        }

        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Message>> GetMessagesBetweenUsersAsync(long userId1, long userId2)
    {
        return await _context
            .Messages
            .Where(
                m =>
                    (m.SenderId == userId1 && m.ReceiverId == userId2)
                    || (m.SenderId == userId2 && m.ReceiverId == userId1)
            )
            .OrderBy(m => m.DateSent)
            .Select(
                m =>
                    new Message
                    {
                        Id = m.Id,
                        Content = m.Content,
                        DateSent = m.DateSent,
                        Edited = m.Edited,
                        SenderId = m.SenderId,
                        Sender = new User { Id = m.Sender.Id, UserName = m.Sender.UserName }
                    }
            )
            .ToListAsync();
    }

    public async Task<List<ChatParticipantDto>> GetChatParticipantsAsync(long userId)
    {
        var sentParticipants = await _context
            .Messages
            .Where(m => m.SenderId == userId)
            .Select(m => new ChatParticipantDto { UserId = m.Id, Username = m.Receiver.UserName })
            .Distinct()
            .ToListAsync();

        var receivedParticipants = await _context
            .Messages
            .Where(m => m.ReceiverId == userId)
            .Select(
                m => new ChatParticipantDto { UserId = m.SenderId, Username = m.Sender.UserName }
            )
            .Distinct()
            .ToListAsync();

        // Combine and remove duplicate participants
        var allParticipants = sentParticipants
            .Concat(receivedParticipants)
            .GroupBy(p => p.UserId)
            .Select(grp => grp.First())
            .ToList();

        return allParticipants;
    }
}
