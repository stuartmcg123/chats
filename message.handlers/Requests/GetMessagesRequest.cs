using MediatR;
using message.dto;

namespace message.handlers.Requests
{
    public record GetMessagesRequest(int Page, int PageSize) : IRequest<IEnumerable<MessageDto>>;
    public record CreateMessagesRequest(CreateMessageDto Message) : IRequest;
    public record DeleteMessagesRequest(string Id) : IRequest;
}
