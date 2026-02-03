using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace AspNetTemplate.WebAPI.Controllers.v1
{
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger Logger;

        protected BaseController(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(this.GetType());
        }

        protected long UserId => GetUserId();

        protected long GetUserId()
        {
            if (!(User?.Identity?.IsAuthenticated ?? false))
            {
                Logger.LogWarning("user not login but try get user info");
                throw new AuthenticationException();
            }

            return long.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
        }
    }
}