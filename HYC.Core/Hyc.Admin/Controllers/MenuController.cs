using Hyc.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hyc.Admin.Models;
using Hyc.Service.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Hyc.Admin.Controllers
{
    //[Authorize]
    //[Authorize(Roles = "Administrator")]
    [Authorize(Policy = "UserOnly")]
    public class MenuController : BaseController
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>

        /// 获取功能树JSON数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMenuTreeData()
        {
            var menus = _menuService.GetAllList();
            List<TreeModel> treeModels = new List<TreeModel>();
            foreach (var menu in menus)
            {
                treeModels.Add(new TreeModel() { Id = menu.Id.ToString(), Text = menu.Name, Parent = menu.ParentId == Guid.Empty ? "#" : menu.ParentId.ToString() });
            }
            return Json(treeModels);
        }

        /// <summary>
        /// 获取子级功能列表
        /// </summary>
        /// <returns></returns>

        public IActionResult GetMneusByParent(Guid parentId, int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = _menuService.GetMenusByParent(parentId, startPage, pageSize, out rowCount);
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result,
            });
        }

        [HttpGet]
        public IActionResult Get(Guid Id)
        {
            var result = _menuService.Get(Id);
            return Json(result);
        }

        [HttpPost]
        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IActionResult Edit(MenuDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = GetModelStateError()
                });
            }
            if (_menuService.InsertOrUpdate(dto))
            {
                return Json(new { Result = "Success" });
            }
            return Json(new { Result = "Faild" });
        }

        public IActionResult DeleteMuti(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                List<Guid> delIds = new List<Guid>();
                foreach (string id in idArray)
                {
                    _menuService.Delete(Guid.Parse(id));
                }
                return Json(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = ex.Message
                });
            }
        }

        public IActionResult Delete(Guid id)
        {
            try
            {
                _menuService.Delete(id);
                return Json(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = ex.Message
                });
            }
        }
    }
}
