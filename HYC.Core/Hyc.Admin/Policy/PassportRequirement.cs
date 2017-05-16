using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hyc.Admin.Policy
{
    public class PassportRequirement :  AuthorizationHandler<PassportRequirement>, IAuthorizationRequirement
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
       
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PassportRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.UserData && c.Issuer == "http://contoso.com"))
            {
                return Task.CompletedTask;
            }
            var UserData = context.User.FindFirst(c => c.Type == ClaimTypes.UserData).Value;
            if (UserData.IndexOf(",1,2,") < 0)
            {
                return Task.CompletedTask;
            }
            //转换成MVC请求上下文
            var mvcContext = context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
            if (mvcContext != null)
            {
                ////Examine MVC specific things like routing data.
                //转换成mvc控制器
                var controllerActionDescriptor = mvcContext.ActionDescriptor as ControllerActionDescriptor;
                requirement.ControllerName = controllerActionDescriptor.ControllerName;
                requirement.ActionName = controllerActionDescriptor.ActionName;
                if (requirement.ControllerName == "Users")
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
