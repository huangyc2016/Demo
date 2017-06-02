using System;
using Hyc.Service.Dtos;
using System.Collections.Generic;
using Hyc.Repository;

namespace Hyc.Service
{
    public interface IUserService
    {
        List<UserDto> GetAllList();

        PageDataView<UserDto> GetList(string userName, int page, int pageSize = 10, string connectionString = null);

        UserDto CheckUser(string UserName, string Password, string connectionString = null);

        /// <summary>
        /// 添加编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool InsertOrUpdate(UserDto dto);

        /// <summary>
        /// 根据Id获取实体菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserDto Get(int id);

        bool Delete(int Id);
    }
}
