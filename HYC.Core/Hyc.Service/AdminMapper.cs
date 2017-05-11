using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hyc.Model.TableModel;
using Hyc.Service.Dtos;

namespace Hyc.Service
{
    public class AdminMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => {
                //菜单
                cfg.CreateMap<Menu, MenuDto>();
                cfg.CreateMap<MenuDto, Menu>();

                //用户
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
            });
        }
    }
}
