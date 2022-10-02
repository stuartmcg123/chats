using message.models;

namespace message.services;
public interface IMessageService
{
    Task<IEnumerable<Message>> GetMessages();

}
