using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hyc.Admin.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hyc.Admin.Controllers
{
    public class LoginController : Controller
    {
        private Service.IUserService _UserTodo { get; set; }
        public LoginController(Service.IUserService userTodo) {
            this._UserTodo = userTodo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            GetUserId();
            if (ModelState.IsValid)
            {
                //检查用户信息
                var user = _UserTodo.CheckUser(model.UserName, model.Password);
                if (user != null)
                {
                    //记录Session
                    /////HttpContext.Session.Set("CurrentUser", Utility.ByteConvertHelper.ObjectToBytes(user));

                    //记录cookie
                    WriteUser(4, model.UserName);

                    //跳转到系统首页
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ErrorInfo = "用户名或密码错误。";
                return View();
            }
            ViewBag.ErrorInfo = ModelState.Values.First().Errors[0].ErrorMessage;

            return View(model);
        }

        private async void WriteUser(int userId, string userName)
        {
            var identity = new ClaimsIdentity("Forms");     // 指定身份认证类型
            identity.AddClaim(new Claim(ClaimTypes.Sid, userId.ToString()));  // 用户Id
            identity.AddClaim(new Claim(ClaimTypes.Name, userName));       // 用户名称
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.Authentication.SignInAsync("UserAuth", 
                principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                });  //过期时间20分钟
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("UserAuth");   // Startup.cs中配置的验证方案名
            return RedirectToAction("Users", "Index");
        }

        private int GetUserId()
        {
            //var userName = User.Identity.Name;  //获取登录时存储的用户名称
            var claim = User.FindFirst(ClaimTypes.Sid); // 获取登录时存储的Id
            if (claim==null)
            {
                return 0;
            }
            else
            {
                return int.Parse(claim.Value);
            }
        }

        public JsonResult CheckLogin()
        {
            var userName = User.Identity.Name;  //获取登录时存储的用户名称
            var userId = User.FindFirst(ClaimTypes.Sid).Value; // 获取登录时存储的Id
            return Json(new
            {
                UserId = userId,
                UserName = userName
            });
        }
    }
}
