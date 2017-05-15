using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hyc.Admin.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //byte[] result;
            //context.HttpContext.Session.TryGetValue("CurrentUser", out result);
            //if (result == null)
            //{
            //    context.Result = new RedirectResult("/Login/Index");
            //    return;
            //}

            var userName = User.Identity.Name;
            var claim = User.FindFirst(ClaimTypes.Sid);
            //var userId = User.FindFirst(ClaimTypes.Sid).Value; // 获取登录时存储的Id
            if (string.IsNullOrEmpty(userName) && claim != null)
            {
                context.Result = new RedirectResult("/Login/Index");
                return;
            }
            base.OnActionExecuting(context);
        }
        /// <summary>
        /// 获取服务端验证的第一条错误信息
        /// </summary>
        /// <returns></returns>
        public string GetModelStateError()
        {
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    return item.Errors[0].ErrorMessage;
                }
            }
            return "";
        }
    }
}
