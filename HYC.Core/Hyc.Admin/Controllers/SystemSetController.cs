using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hyc.Service.Dtos;
using Hyc.Service;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hyc.Admin.Controllers
{
    public class SystemSetController : BaseController
    {
        private readonly ILogger<SystemSetController> _logger;
        private readonly IControllerService _controllerService;
        private readonly IActionService _actionService;
        public SystemSetController(ILogger<SystemSetController> logger,IControllerService controllerService, IActionService actionService)
        {
            this._logger = logger;
            this._controllerService = controllerService;
            this._actionService = actionService;
        }

        #region ===控制器管理===
        // GET: /<controller>/
        public IActionResult ControllerIndex()
        {
            this._logger.LogInformation("THis Is Test");
            ViewBag.CurrentMenu = "SystemSetControllerIndex";
            return View();
        }

        [HttpGet]
        public IActionResult GetControllerList()
        {
            var result = _controllerService.GetAllList();
            return Json(new
            {
                rows = result,
            });
        }

        [HttpGet]
        public IActionResult GetControllerById(int Id)
        {
            var result = _controllerService.Get(Id);
            return Json(result);
        }


        [HttpPost]
        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IActionResult EditController(ControllerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = GetModelStateError()
                });
            }
            if (_controllerService.InsertOrUpdate(dto))
            {
                return Json(new { Result = "Success", Message = "保存成功" });
            }
            return Json(new { Result = "Faild", Message = "保存失败" });
        }

        [HttpPost]
        public IActionResult DeleteMutiController(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                foreach (string id in idArray)
                {
                    _controllerService.Delete(int.Parse(id));
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
        public IActionResult DeleteController(int id)
        {
            try
            {
                _controllerService.Delete(id);
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
        # endregion

        #region ===Action管理===
        // GET: /<controller>/
        public IActionResult ActionIndex(int ControllerId = 0)
        {
            ViewBag.CurrentMenu = "SystemSetActionIndex";
            var ControllerList = _controllerService.GetAllList();
            ViewBag.ControllerId = ControllerId;
            ViewData["ControllerList"] = ControllerList;
            return View();
        }

        [HttpGet]
        public IActionResult GetActionList(int ControllerId)
        {
            var result = _actionService.GetAllList(ControllerId);
            return Json(new
            {
                rows = result,
            });
        }

        [HttpGet]
        public IActionResult GetActionById(int Id)
        {
            var result = _actionService.Get(Id);
            return Json(result);
        }


        [HttpPost]
        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IActionResult EditAction(ActionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = GetModelStateError()
                });
            }
            if (_actionService.InsertOrUpdate(dto))
            {
                return Json(new { Result = "Success", Message = "保存成功" });
            }
            return Json(new { Result = "Faild", Message = "保存失败" });
        }

        [HttpPost]
        public IActionResult DeleteMutiAction(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                foreach (string id in idArray)
                {
                    _actionService.Delete(int.Parse(id));
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
        public IActionResult DeleteAction(int id)
        {
            try
            {
                _actionService.Delete(id);
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
        # endregion
    }
}
