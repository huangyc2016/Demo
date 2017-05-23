using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.Model.TableModel
{
    /// <summary>
    /// 菜单授权类
    /// </summary>
    public class MenuAuthorize
    {
        /// <summary>
        /// 菜单授权Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
    }
}
