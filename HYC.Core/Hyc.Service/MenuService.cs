using System;
using System.Collections.Generic;
using System.Text;
using Hyc.Service.Dtos;
using AutoMapper;
using Hyc.Repository;

namespace Hyc.Service
{
    public class MenuService: IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public List<MenuDto> GetAllList()
        {
            var menus = _menuRepository.RetriveAllEntity();

            //使用AutoMapper进行实体转换
            return Mapper.Map<List<MenuDto>>(menus);
        }
    }
}
