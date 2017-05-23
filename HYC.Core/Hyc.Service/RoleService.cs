using System;
using System.Collections.Generic;
using System.Text;
using Hyc.Service.Dtos;
using AutoMapper;
using Hyc.Repository;
using Hyc.Model.TableModel;

namespace Hyc.Service
{
    public class RoleService: IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public List<RoleDto> GetAllList()
        {

            var users = _roleRepository.RetriveAllEntity();

            //使用AutoMapper进行实体转换
            return Mapper.Map<List<RoleDto>>(users);
        }


        public RoleDto Get(int id)
        {
            var menus = _roleRepository.RetriveOneEntityById(id);
            return Mapper.Map<RoleDto>(menus);
        }

        public bool InsertOrUpdate(RoleDto dto)
        {
            if (dto.Id > 0)
            {
                return _roleRepository.UpdateEntity(Mapper.Map<Role>(dto));
            }
            else
            {
                return _roleRepository.InsertEntity(Mapper.Map<Role>(dto));
            }
        }

        public bool Delete(int Id)
        {
            return _roleRepository.DeleteEntityById(Id);
        }
    }
}
