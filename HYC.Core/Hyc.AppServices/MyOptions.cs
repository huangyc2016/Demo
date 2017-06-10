using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.AppServices
{
    public class ConnectionOptions
    {
        public string sqlConnectionHyc { get; set; }
    }
    public class HandlerOptions
    {
        /// <summary>
        /// 服务名称(需要唯一)
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 运行时间方式(month,week,day,hour,minute,second)
        /// </summary>
        public string Interval { get; set; }

        /// <summary>
        /// 在某天
        /// </summary>
        public int AtDay { get; set; }

        /// <summary>
        /// 在某小时
        /// </summary>
        public int AtHour { get; set; }

        /// <summary>
        /// 在某分钟
        /// </summary>
        public int AtMinute { get; set; }

        /// <summary>
        /// 多少分钟或者多少秒运行一次
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 服务是否启用
        /// </summary>
        public bool IsOpen { get; set; }

    }
}
