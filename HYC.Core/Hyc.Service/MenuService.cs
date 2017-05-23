using System;
using System.Collections.Generic;
using System.Text;
using Hyc.Service.Dtos;
using AutoMapper;
using Hyc.Repository;
using Hyc.Model.TableModel;

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

        public List<MenuDto> GetMenusByParent(int parentId, int startPage, int pageSize, out int rowCount)
        {
            var menus = _menuRepository.LoadPageList(parentId, startPage, pageSize, out rowCount);
            return Mapper.Map<List<MenuDto>>(menus);
        }

        public MenuDto Get(int id)
        {
            var menus = _menuRepository.RetriveOneEntityById(id);
            return Mapper.Map<MenuDto>(menus);
        }

        public bool InsertOrUpdate(MenuDto dto)
        {
            var parent = _menuRepository.RetriveOneEntityById(dto.ParentId);
            if (parent == null)
            {
                dto.DepthNum = 0;
            }
            else {
                dto.DepthNum = parent.DepthNum + 1;
            }
            if (dto.Id>0)
            {
                return _menuRepository.UpdateEntity(Mapper.Map<Menu>(dto));
            }
            else
            {
                return _menuRepository.InsertEntity(Mapper.Map<Menu>(dto));
            }
        }

        public bool Delete(int Id)
        {
            return _menuRepository.DeleteEntityById(Id);
        }
    }
}
