using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.Service.Dtos
{
    /// <summary>
    /// 功能授权类
    /// </summary>
    public class ActionAuthorizeDto
    {
        /// <summary>
        /// 功能授权Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 菜单授权Id
        /// </summary>
        public int MenuAuthorizeId { get; set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        public int ActionId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
    }
}
