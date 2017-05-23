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
            if (ModelState.IsValid)
            {
                //检查用户信息
                var user = _UserTodo.CheckUser(model.UserName, model.Password);
                if (user != null)
                {
                    //记录Session
                    /////HttpContext.Session.Set("CurrentUser", Utility.ByteConvertHelper.ObjectToBytes(user));

                    //记录cookie
                    WriteUser(4, model.UserName, model.Password);
                    //跳转到系统首页
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ErrorInfo = "用户名或密码错误。";
                return View();
            }
            ViewBag.ErrorInfo = ModelState.Values.First().Errors[0].ErrorMessage;

            return View(model);
        }

        private async void WriteUser(int userId, string userName,string password)
        {
            List<Claim> claims = new List<Claim>();//定义声明
            claims.Add(new Claim(ClaimTypes.Name, userName, ClaimValueTypes.String, "http://contoso.com"));//管理员名称
            claims.Add(new Claim(ClaimTypes.Role, "Administrator", ClaimValueTypes.String, "http://contoso.com"));//管理员角色
            claims.Add(new Claim("UserId", userId.ToString(), ClaimValueTypes.String, "http://contoso.com"));//管理员帐号
            claims.Add(new Claim(ClaimTypes.DateOfBirth, "2000-01-01", ClaimValueTypes.Date, "http://contoso.com"));//日期
            claims.Add(new Claim(ClaimTypes.UserData, ",1,2,", ClaimValueTypes.String, "http://contoso.com"));//可以访问的资源
            //claims.Add(new Claim(ClaimTypes.UserData, "[{ControllerName:Users,ActionName:Index},{ControllerName:Menu,ActionName:Index}]", ClaimValueTypes.String, "http://contoso.com"));//可以访问的资源
            //claims.Add(new Claim("UsernameAndPassword", userName + password, ClaimValueTypes.String, "http://contoso.com"));//用户名和密码
            //claims.Add(new Claim("AccessToken", DateTime.Now.AddMinutes(1).ToString(), ClaimValueTypes.String, "http://contoso.com"));//访问token
            var userIdentity = new ClaimsIdentity("Forms");
            userIdentity.AddClaims(claims);
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            await HttpContext.Authentication.SignInAsync("UserAuth", userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("UserAuth");   // Startup.cs中配置的验证方案名
            return RedirectToAction("Login", "Index");
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

        public IActionResult Forbidden()
        {
            return View();
        }


    }
}
