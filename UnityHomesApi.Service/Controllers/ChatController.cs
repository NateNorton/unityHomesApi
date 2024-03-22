using System.Security.Claims;
using HomesApi.Data.Repositories;
using HomesApi.Dtos;
using HomesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace unityHomesApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatRepository _chatRepository;

    public ChatController(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    // POST api/messages
    [HttpPost]
    public async Task<IActionResult> PostMessage([FromBody] MessageCreateDto messageDto)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return Unauthorized("Invalid user id");
        }

        var message = new Message
        {
            Content = messageDto.Content,
            SenderId = userId,
            ReceiverId = messageDto.ReceipientId,
            DateSent = DateTime.Now,
            Edited = false
        };

        try
        {
            var createdMessage = await _chatRepository.AddMessageAsync(message);
            if (createdMessage == null)
            {
                return BadRequest("Failed to send message");
            }
            return Ok(createdMessage);
        }
        catch (Exception)
        {
            return StatusCode(500, "Failed to send message");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMessage(long id)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return Unauthorized("Invalid user id");
        }

        var message = await _chatRepository.GetMessageByIdAsync(id);

        if (message == null)
        {
            return NotFound("Message not found");
        }

        if (message.SenderId != userId && message.ReceiverId != userId)
        {
            return Unauthorized("You are not authorized to view this message");
        }

        return Ok(message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditMessage(long id, [FromBody] MessageEditDto messageEditDto)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return Unauthorized("Invalid user id");
        }

        var message = await _chatRepository.GetMessageByIdAsync(id);

        if (message == null)
        {
            return NotFound("Message not found");
        }

        if (message.SenderId != userId)
        {
            return Unauthorized("You are not authorized to edit this message");
        }

        bool updatedMessage = await _chatRepository.EditMessageAsync(
            id,
            messageEditDto.Content ?? ""
        );

        if (!updatedMessage)
        {
            return StatusCode(500, "Failed to edit message");
        }

        return Ok("Message edited successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessage(long id)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(userIdString, out long userId))
        {
            return Unauthorized("Invalid user id");
        }

        var message = await _chatRepository.GetMessageByIdAsync(id);

        if (message == null)
        {
            return NotFound("Message not found");
        }

        if (message.SenderId != userId)
        {
            return Unauthorized("You are not authorized to delete this message");
        }

        bool deletedMessage = await _chatRepository.DeleteMessageAsync(id);

        if (!deletedMessage)
        {
            return StatusCode(500, "Failed to delete message");
        }

        return Ok("Message deleted successfully");
    }
}
