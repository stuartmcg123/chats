using MediatR;
using message.dto;
using message.handlers.Notifications;
using message.handlers.Requests;
using message.services;

namespace message.handlers
{
    public class GetMessagesRequestHandler : IRequestHandler<GetMessagesRequest, IEnumerable<MessageDto>>, IRequestHandler<DeleteMessagesRequest>, IRequestHandler<CreateMessagesRequest>
    {
        private readonly IMessageService _messageService;
        private readonly IMediator _mediator;

        public GetMessagesRequestHandler(
            IMessageService messageService,
            IMediator mediator)
        {
            _messageService = messageService;
            _mediator = mediator;
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
            var message = await _messageService.AddMessage(request.message);

            await _mediator.Publish(new NewMessageNotification(message));

            return Unit.Value;
        }
    }
}