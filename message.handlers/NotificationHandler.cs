using MediatR;
using message.handlers.Notifications;
using message.services;
using Microsoft.AspNetCore.SignalR;

namespace message.handlers
{
    public class NotificationHandler : INotificationHandler<NewMessageNotification>
    {
        private readonly IHubContext<MessageHub> _messageHub;

        public NotificationHandler(IHubContext<MessageHub> messageHub)
        {
            _messageHub = messageHub;
        }
        public async Task Handle(NewMessageNotification notification, CancellationToken cancellationToken)
        {
            await _messageHub
                 .Clients.All.SendAsync("newMessage", notification, cancellationToken);
        }
    }
}