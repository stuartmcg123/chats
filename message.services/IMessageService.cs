using message.dto;
using message.models;

namespace message.services;
public interface IMessageService
{
    Task<MessageDto> AddMessage(CreateMessageDto createMessageDto);
    Task DeleteMessage(string id);
    Task<IEnumerable<MessageDto>> GetMessages();

}
