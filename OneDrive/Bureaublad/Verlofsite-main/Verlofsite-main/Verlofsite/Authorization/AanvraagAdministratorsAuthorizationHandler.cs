using System.Threading.Tasks;
using Verlofsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Verlofsite.Authorization;

namespace AanvraagManager.Authorization
{
    public class AanvraagAdministratorsAuthorizationHandler
                    : AuthorizationHandler<OperationAuthorizationRequirement, Aanvraag>
    {
        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                     Aanvraag resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.AanvraagAdministratorsRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}