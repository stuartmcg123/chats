using message.dto;
using message.models;

namespace message.services;
public interface IMessageService
{
    /// <summary>
    /// Save a new message
    /// </summary>
    /// <param name="createMessageDto"></param>
    /// <returns></returns>
    Task<MessageDto> AddMessage(CreateMessageDto createMessageDto);

    /// <summary>
    /// Delete a message.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteMessage(string id);

    /// <summary>
    /// Get user's messages.s
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<MessageDto>> GetMessages(int page = 0, int pageSize = 50);

}
