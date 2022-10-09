using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace message.utils
{
    public class UserIdentityProvider : IUserIdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdentityProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Get()
        {
         var name = _httpContextAccessor
                 .HttpContext
                 .User
                 .Claims
                 .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

            return name.Value;
        }
    }
}