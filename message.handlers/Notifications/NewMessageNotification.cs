using MediatR;
using message.dto;

namespace message.handlers.Notifications
{
    public record NewMessageNotification(MessageDto Dto) : INotification;
}
