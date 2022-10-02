using message.dto;
using message.models;

namespace message.services;
public interface IMessageService
{
    Task AddMessage(CreateMessageDto createMessageDto);
    Task DeleteMessage(string id);
    Task<IEnumerable<MessageDto>> GetMessages();

}
