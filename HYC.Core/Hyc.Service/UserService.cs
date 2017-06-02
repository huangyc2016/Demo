using System;
using System.Collections.Generic;
using System.Text;
using Hyc.Service.Dtos;
using AutoMapper;
using Hyc.Repository;
using Hyc.Model.TableModel;

namespace Hyc.Service
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public List<UserDto> GetAllList()
        {

            var users = _userRepository.RetriveAllEntity();

            //使用AutoMapper进行实体转换
            return Mapper.Map<List<UserDto>>(users);
        }

        public PageDataView<UserDto> GetList(string userName, int page, int pageSize = 10, string connectionString = null)
        {

            var users = _userRepository.GetList(userName, page, pageSize, connectionString);
            
            //使用AutoMapper进行实体转换
            return Mapper.Map<PageDataView<UserDto>>(users);
        }

        public UserDto CheckUser(string UserName, string Password, string connectionString = null)
        {

            var users = _userRepository.CheckUser(UserName, Password, connectionString);

            //使用AutoMapper进行实体转换
            return Mapper.Map<UserDto>(users);
        }

        public UserDto Get(int id)
        {
            var menus = _userRepository.RetriveOneEntityById(id);
            return Mapper.Map<UserDto>(menus);
        }

        public bool InsertOrUpdate(UserDto dto)
        {
            if (dto.Id > 0)
            {
                return _userRepository.UpdateEntity(Mapper.Map<User>(dto));
            }
            else
            {
                return _userRepository.InsertEntity(Mapper.Map<User>(dto));
            }
        }

        public bool Delete(int Id)
        {
            return _userRepository.DeleteEntityById(Id);
        }
    }
}
