using AutoMapper;
using message.dto;
using message.models;
using message.utils;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace message.services;

internal sealed class MessageService : IMessageService
{
    private readonly IMongoCollection<Message> _messages;
    private readonly IUserIdentityProvider _userIdentityProvider;
    private readonly IMapper _mapper;
    private readonly ILogger<MessageService> _logger;

    private string _userId => _userIdentityProvider.Get();

    public MessageService(
        IMongoCollection<Message> messages,
        IUserIdentityProvider userIdentityProvider,
        IMapper mapper,
        ILogger<MessageService> logger)
    {
        _messages = messages;
        _userIdentityProvider = userIdentityProvider;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<MessageDto>> GetMessages(int page = 0, int pageSize = 50)
    {
        var messages = _messages
             .Find(Builders<Message>.Filter.Empty)
             .Sort(Builders<Message>.Sort.Descending(c => c.Created))
             .Skip(page * pageSize)             
             .Limit(pageSize);


        var castedMessages = await messages.Project(
            Builders<Message>
            .Projection
            .Expression(m => _mapper.Map<MessageDto>(m))).ToListAsync();

        castedMessages.Reverse();

        return castedMessages;
    }

    public async Task<MessageDto> AddMessage(CreateMessageDto createMessageDto)
    {
        try
        {
            var message = _mapper.Map<Message>(createMessageDto);

            await _messages
                .InsertOneAsync(message);

            return _mapper.Map<MessageDto>(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to map dto to message");
            throw;
        }
    }

    public async Task DeleteMessage(string id)
    {
        await _messages
               .DeleteOneAsync(Builders<Message>.Filter.And(
                   Builders<Message>.Filter.Eq(c => c.Id, id)));
    }
}
