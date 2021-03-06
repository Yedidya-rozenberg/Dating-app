using System;
using System.Threading.Tasks;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
                var resultContext = await next();
                if(!resultContext.HttpContext.User.Identity.IsAuthenticated) return;
                
                var userId= resultContext.HttpContext.User.GetUserId();

                var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();

                var user = await repo.GetUserByIdAsync(userId);

                var lastActive = DateTime.Now;

                user.LastActive = lastActive;

                await repo.SaveAllAsync();


        }
    }
}