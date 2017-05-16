
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hyc.Admin.Policy
{
    public class MinimumAgeRequirement : AuthorizationHandler<MinimumAgeRequirement>, IAuthorizationRequirement
    {
        int _minimumAge;
        public MinimumAgeRequirement(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth && c.Issuer == "http://contoso.com"))
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            if (calculatedAge >= requirement._minimumAge)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
