using Hyc.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hyc.Admin.Components
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent:ViewComponent
    {
        private readonly IMenuService _menuService;
        private readonly IUserService _userService;
        public NavigationViewComponent(IMenuService menuService, IUserService userService)
        {
            _menuService = menuService;
            _userService = userService;
        }

        public IViewComponentResult Invoke()
        {
            //var userId = HttpContext.Session.GetString("CurrentUserId");
            //var menus = _menuService.GetMenusByUser(Guid.Parse(userId));
            var menus = _menuService.GetAllList();
            return View(menus);
        }

    }
}
