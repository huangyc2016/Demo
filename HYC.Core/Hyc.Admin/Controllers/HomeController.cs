using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Hyc.Service;

namespace Hyc.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IOptions<MyOptions> _optionsAccessor;
        private readonly IUserService _userService;

        public HomeController(IOptions<MyOptions> optionsAccessor, IUserService userService)
        {
            this._optionsAccessor = optionsAccessor;
            this._userService = userService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "首页";
            ViewBag.CurrentMenu = "HomeIndex";
            var sqlconnection = _optionsAccessor.Value.sqlConnectionHyc;
            var result = _userService.GetList("", 1, 10, sqlconnection);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
