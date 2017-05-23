using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.Model.TableModel
{
    /// <summary>
    /// 功能组类
    /// </summary>
    public class ActionGroup
    {
        /// <summary>
        /// 功能组Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 功能组名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 功能组描述
        /// </summary>
        public string Description { get; set; }
    }
}
