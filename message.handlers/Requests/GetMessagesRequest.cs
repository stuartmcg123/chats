using MediatR;
using message.dto;

namespace message.handlers.Requests
{
    public record GetMessagesRequest() : IRequest<IEnumerable<MessageDto>>;
    public record CreateMessagesRequest(CreateMessageDto message) : IRequest;
    public record DeleteMessagesRequest(string Id) : IRequest;
}
