using System;
using System.Collections.Generic;
using System.Text;
using Hyc.Service.Dtos;
using AutoMapper;
using Hyc.Repository;

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

        public UserDto CheckUser(string UserName, string Password, string connectionString = null)
        {

            var users = _userRepository.CheckUser(UserName, Password, connectionString);

            //使用AutoMapper进行实体转换
            return Mapper.Map<UserDto>(users);
        }
    }
}
