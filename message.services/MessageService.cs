using message.models;
using MongoDB.Driver;

namespace message.services;

internal sealed class MessageService : IMessageService
{
    private readonly IMongoCollection<Message> _messages;

    public MessageService(IMongoCollection<Message> messages)
    {
        _messages = messages;
    }

    public async Task<IEnumerable<Message>> GetMessages()
    {
        var messages = _messages
             .Find(_ => true);

        return await messages.ToListAsync();
    }

    public async Task AddMessage()
    {

    }
}
