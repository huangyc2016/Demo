using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hyc.Service;
using Hyc.Service.Dtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hyc.Admin.Controllers
{
    public class AccountController :  BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public AccountController(IUserService userService, IRoleService roleService)
        {
            this._userService = userService;
            this._roleService = roleService;
        }
        // GET: /<controller>/
        public IActionResult RoleIndex()
        {
            ViewBag.CurrentMenu = "AccountRoleIndex";
            return View();
        }

        public IActionResult GetRoleList(int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = _roleService.GetAllList();
            rowCount = result.Count();
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result,
            });
        }

        [HttpGet]
        public IActionResult GetRoleById(int Id)
        {
            var result = _roleService.Get(Id);
            return Json(result);
        }


        [HttpPost]
        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IActionResult EditRole(RoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = GetModelStateError()
                });
            }
            if (_roleService.InsertOrUpdate(dto))
            {
                return Json(new { Result = "Success", Message = "保存成功" });
            }
            return Json(new { Result = "Faild", Message = "保存失败" });
        }

        [HttpPost]
        public IActionResult DeleteMutiRole(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                foreach (string id in idArray)
                {
                    _roleService.Delete(int.Parse(id));
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

        [HttpPost]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                _roleService.Delete(id);
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

        #region
        public IActionResult UserIndex()
        {
            ViewBag.CurrentMenu = "AccountUserIndex";
            return View();
        }

        [HttpGet]
        public IActionResult GetUserList(int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = _userService.GetList("",startPage,pageSize);
            rowCount = result.TotalPageCount;
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result.Items,
            });
        }

        [HttpGet]
        public IActionResult GetUserbyId(int Id)
        {
            var result = _userService.Get(Id);
            return Json(result);
        }

        public IActionResult EditUser(UserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = GetModelStateError()
                });
            }
            if (_userService.InsertOrUpdate(dto))
            {
                return Json(new { Result = "Success", Message = "保存成功" });
            }
            return Json(new { Result = "Faild", Message = "保存失败" });
        }
        #endregion
    }
}
