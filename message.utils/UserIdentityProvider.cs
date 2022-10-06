using Microsoft.AspNetCore.Http;

namespace message.utils
{
    public class UserIdentityProvider : IUserIdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdentityProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Get() => _httpContextAccessor
                 .HttpContext
                 .User
                 .Identity
                 .Name;
    }
}