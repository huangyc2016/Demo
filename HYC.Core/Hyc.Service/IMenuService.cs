using System;
using Hyc.Service.Dtos;
using System.Collections.Generic;

namespace Hyc.Service
{
    public interface IMenuService
    {
        List<MenuDto> GetAllList();
    }
}
