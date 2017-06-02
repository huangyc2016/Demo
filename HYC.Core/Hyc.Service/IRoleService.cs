using System;
using Hyc.Service.Dtos;
using System.Collections.Generic;

namespace Hyc.Service
{
    public interface IRoleService
    {
        List<RoleDto> GetAllList();

        /// <summary>
        /// 添加编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool InsertOrUpdate(RoleDto dto);

        /// <summary>
        /// 根据Id获取实体菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleDto Get(int id);

        bool Delete(int Id);
    }
}
