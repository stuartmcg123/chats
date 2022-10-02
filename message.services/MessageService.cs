using message.dto;
using message.models;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace message.services;

internal sealed class MessageService : IMessageService
{
    private readonly IMongoCollection<Message> _messages;
    private readonly HttpContext _context;
    private string _userId => _context.User.Identity.Name;
    public MessageService(IMongoCollection<Message> messages,
        IHttpContextAccessor httpContextAccessor)
    {
        _messages = messages;
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<IEnumerable<MessageDto>> GetMessages()
    {
        var messages = _messages
             .Find(Builders<Message>.Filter.Empty)
             .Sort(Builders<Message>.Sort.Descending(c => c.Created));

        return await messages.Project<MessageDto>(
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

    public async Task AddMessage(CreateMessageDto createMessageDto)
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
    }

    public async Task DeleteMessage(string id)
    {
        await _messages
               .DeleteOneAsync(Builders<Message>.Filter.And(
                   Builders<Message>.Filter.Eq(c => c.Id, id),
                   Builders<Message>.Filter.Eq(c => c.From, id)));
    }
}
