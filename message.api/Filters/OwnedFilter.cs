using message.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace message.api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OwnedFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IMessageService _messageService;

        public OwnedFilterAttribute(
            IHttpContextAccessor accessor,
            IMessageService messageService)
        {
            _accessor = accessor;
            _messageService = messageService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string messageId = context.RouteData.Values["id"].ToString();
            var userIdClaim = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var message = await _messageService
                .GetMessage(messageId);

            if (message.From != userIdClaim.Value)
                context.Result = new ForbidResult();
            await Task.CompletedTask;
        }
    }
}
