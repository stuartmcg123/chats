using MediatR;
using message.handlers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace message.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
