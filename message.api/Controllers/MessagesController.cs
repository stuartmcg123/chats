using MediatR;
using message.dto;
using message.handlers.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace message.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var messages = await _mediator
                   .Send(new GetMessagesRequest());

            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateMessageDto dto)
        {
            await _mediator.Send(new CreateMessagesRequest(dto));

            return NoContent();
        }
    }
}
