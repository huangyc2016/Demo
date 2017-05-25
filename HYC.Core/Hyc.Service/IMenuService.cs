using System;
using Hyc.Service.Dtos;
using System.Collections.Generic;

namespace Hyc.Service
{
    public interface IMenuService
    {
        List<MenuDto> GetAllList();

        /// <summary>
        /// 根据父级Id获取功能列表
        /// </summary>
        /// <param name="parentId">父级Id</param>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="rowCount">数据总数</param>
        /// <returns></returns>
        List<MenuDto> GetMenusByParent(int parentId, int startPage, int pageSize, out int rowCount);

        /// <summary>
        /// 添加或者修改菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool InsertOrUpdate(MenuDto dto);

        /// <summary>
        /// 根据Id获取实体菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MenuDto Get(int id);

        bool Delete(int Id);

        /// <summary>
        /// 根据层级编号获取菜单
        /// </summary>
        /// <param name="depthNum"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        List<MenuDto> GetListByDepthNum(int depthNum, string connectionString = null);

        /// <summary>
        /// 根据父级Id获取菜单
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        List<MenuDto> GetListByParentId(int parentId, string connectionString = null);
    }
}
