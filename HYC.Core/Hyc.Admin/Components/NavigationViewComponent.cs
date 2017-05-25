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
            //ca
            int depthNum = 1;
            var menus = _menuService.GetListByDepthNum(depthNum);
            foreach (var item in menus)
            {
                List<string> codelist = new List<string>();
                var submenus = _menuService.GetListByParentId(item.Id);
                foreach (var subitem in submenus)
                {
                    codelist.Add(subitem.Code);
                }
                if(codelist.Count>0)
                {
                    item.Code = string.Join(",", codelist);
                }
                item.SubMeunList = submenus;
            }
            return View(menus);
        }

    }
}
