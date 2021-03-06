﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.Model.TableModel
{
    /// <summary>
    /// 用户角色类
    /// </summary>
    public class Role
    {
        /// <summary>
        ///角色Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
    }
}
