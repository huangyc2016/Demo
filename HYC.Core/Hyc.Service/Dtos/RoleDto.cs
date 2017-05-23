using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hyc.Service.Dtos
{
    /// <summary>
    /// 用户角色类
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        ///角色Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        [StringLength(20,MinimumLength =1,ErrorMessage ="角色名称长度必须是1到20之间")]
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [Required(ErrorMessage = "角色描述不能为空")]
        [StringLength(20, MinimumLength = 1)]
        public string Description { get; set; }
    }
}
