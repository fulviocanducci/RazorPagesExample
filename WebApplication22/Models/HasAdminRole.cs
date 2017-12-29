using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace WebApplication22.Models
{
    public class HasAdminRole : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            AdminRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);                
            }
            return Task.CompletedTask;
        }
    }
}
