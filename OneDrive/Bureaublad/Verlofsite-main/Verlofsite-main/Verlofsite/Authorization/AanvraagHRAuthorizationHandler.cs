using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Verlofsite.Models;

namespace Verlofsite.Authorization
{
    public class AanvraagHRAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Aanvraag>
    { 
        protected override Task
          HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Aanvraag resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }


            // Managers can approve or reject.
            if (context.User.IsInRole(Constants.AanvraagHRsRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}