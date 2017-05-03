using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.Model.TableModel
{
    public class User : BaseModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 性别（0女，1男）
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// 出生年月日
        /// </summary>
        public DateTime Birthday { get; set; }
    }
}
