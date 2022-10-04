using message.dto;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace message.services
{
    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;

        public MessageHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task Abc(CreateMessageDto dto)
        {
          var message =  await _messageService
                    .AddMessage(dto);

            await Clients.All.SendAsync("NewMessage", message);
        }
    }
}
