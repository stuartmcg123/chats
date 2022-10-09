using MediatR;
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
        public async Task<IActionResult> Get(int page = 0, int pageSize = 5)
        {
            var messages = await _mediator
                   .Send(new GetMessagesRequest(page, pageSize));

            return Ok(messages);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator
                .Send(new DeleteMessagesRequest(id));

            return NoContent();
        }
    }
}
