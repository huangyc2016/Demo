using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.Service.Dtos
{
    /// <summary>
    /// 控制器类
    /// </summary>
    public class ControllerDto
    {
        /// <summary>
        /// 控制器Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 控制器描述
        /// </summary>
        public string Description { get; set; }
    }
}
