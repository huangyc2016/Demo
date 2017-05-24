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

                //用户角色
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<RoleDto, Role>();

                //控制器
                cfg.CreateMap<Controllers, ControllerDto>();
                cfg.CreateMap<ControllerDto, Controllers>();

                //功能项
                cfg.CreateMap<Actions, ActionDto>();
                cfg.CreateMap<ActionDto, Actions>();
            });
        }
    }
}
