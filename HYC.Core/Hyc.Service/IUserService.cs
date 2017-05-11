using System;
using Hyc.Service.Dtos;
using System.Collections.Generic;

namespace Hyc.Service
{
    public interface IUserService
    {
        List<UserDto> GetAllList();
        UserDto CheckUser(string UserName, string Password, string connectionString = null);
    }
}
