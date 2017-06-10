using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.App.Actions
{
    /// <summary>
    /// 应用程序功能接口
    /// </summary>
    public interface IActions
    {
        /// <summary>
        /// 启动运行方法
        /// </summary>
        /// <returns></returns>
        Object Execute();
    }
}
