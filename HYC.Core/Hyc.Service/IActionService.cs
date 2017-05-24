using System;
using Hyc.Service.Dtos;
using System.Collections.Generic;

namespace Hyc.Service
{
    public interface IActionService
    {
        List<ActionDto> GetAllList(int ControllerId);

        /// <summary>
        /// 添加或者修改菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool InsertOrUpdate(ActionDto dto);

        /// <summary>
        /// 根据Id获取实体菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionDto Get(int id);

        bool Delete(int Id);
    }
}
