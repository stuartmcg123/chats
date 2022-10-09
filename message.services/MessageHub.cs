using message.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace message.services
{
    [Authorize]
    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;

        public MessageHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessage(CreateMessageDto dto)
        {
          var message =  await _messageService
                    .AddMessage(dto);

            await Clients.All.SendAsync("NewMessage", message);
        }
    }
}
