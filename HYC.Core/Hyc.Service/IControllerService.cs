using System;
using Hyc.Service.Dtos;
using System.Collections.Generic;

namespace Hyc.Service
{
    public interface IControllerService
    {
        List<ControllerDto> GetAllList();

        /// <summary>
        /// 添加或者修改菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool InsertOrUpdate(ControllerDto dto);

        /// <summary>
        /// 根据Id获取实体菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ControllerDto Get(int id);

        bool Delete(int Id);
    }
}
