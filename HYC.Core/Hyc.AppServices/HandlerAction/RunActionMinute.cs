using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.AppServices.HandlerAction
{
    public class RunActionMinute:IRunAction
    {
        public void Run(IJob obj, HandlerOptions handler)
        {
            Console.WriteLine($"服务{handler.ActionName}已经启动");
            Action<Schedule> runSchedule = new Action<Schedule>(r=>r.WithName(handler.ActionName)
            .NonReentrant()
            .ToRunNow()
            .AndEvery(handler.Times)
            .Minutes());
            JobManager.AddJob(obj, runSchedule);
        }
    }
}
