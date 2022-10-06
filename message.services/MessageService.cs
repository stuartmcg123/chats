using message.dto;
using message.models;
using message.utils;
using MongoDB.Driver;

namespace message.services;

internal sealed class MessageService : IMessageService
{
    private readonly IMongoCollection<Message> _messages;
    private readonly IUserIdentityProvider _userIdentityProvider;

    private string _userId => _userIdentityProvider.Get();

    public MessageService(
        IMongoCollection<Message> messages,
        IUserIdentityProvider userIdentityProvider)
    {
        _messages = messages;
        _userIdentityProvider = userIdentityProvider;
    }

    public async Task<IEnumerable<MessageDto>> GetMessages()
    {
        var messages = _messages
             .Find(Builders<Message>.Filter.Empty)
             .Sort(Builders<Message>.Sort.Ascending(c => c.Created));

        return await messages.Project(
            Builders<Message>
            .Projection
            .Expression(m => new MessageDto
        {
            Id = m.Id, 
            Body = m.Body,
            Created = m.Created,
            From = m.From
        })).ToListAsync();
    }

    public async Task<MessageDto> AddMessage(CreateMessageDto createMessageDto)
    {
        var message = new Message()
        {
            Body = createMessageDto.Body,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            From = _userId
        };

        await _messages
            .InsertOneAsync(message);

        return new MessageDto
        {
            Id = message.Id,
            Created = message.Created,
            Body = message.Body,
            From = message.From
        };
    }

    public async Task DeleteMessage(string id)
    {
        await _messages
               .DeleteOneAsync(Builders<Message>.Filter.And(
                   Builders<Message>.Filter.Eq(c => c.Id, id),
                   Builders<Message>.Filter.Eq(c => c.From, id)));
    }
}
