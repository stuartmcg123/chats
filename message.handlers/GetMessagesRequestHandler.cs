using MediatR;
using message.dto;
using message.handlers.Requests;
using message.services;

namespace message.handlers
{
    public class GetMessagesRequestHandler : IRequestHandler<GetMessagesRequest, IEnumerable<MessageDto>>, IRequestHandler<DeleteMessagesRequest>, IRequestHandler<CreateMessagesRequest>
    {
        private readonly IMessageService _messageService;

        public GetMessagesRequestHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IEnumerable<MessageDto>> Handle(GetMessagesRequest request, CancellationToken cancellationToken)
        {
            return await _messageService
                .GetMessages();
        }

        public async Task<Unit> Handle(DeleteMessagesRequest request, CancellationToken cancellationToken)
        {
            await _messageService.DeleteMessage(request.Id);

            return Unit.Value;
        }

        public async Task<Unit> Handle(CreateMessagesRequest request, CancellationToken cancellationToken)
        {
            await _messageService.AddMessage(request.message);

            return Unit.Value;
        }
    }
}