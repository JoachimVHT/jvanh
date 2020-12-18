using Verlofsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using Verlofsite.Authorization;

namespace AanvraagManager.Authorization
{
    public class AanvraagIsOwnerAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, Aanvraag>
    {
        UserManager<Verlofsite.Areas.Identity.Data.VerlofsiteUser> _userManager;

        public AanvraagIsOwnerAuthorizationHandler(UserManager<Verlofsite.Areas.Identity.Data.VerlofsiteUser>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Aanvraag resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for CRUD permission, return.

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.OwnerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}