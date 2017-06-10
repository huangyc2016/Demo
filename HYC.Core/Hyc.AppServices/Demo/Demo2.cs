using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.AppServices.Demo
{
    public class Demo2 : IJob
    {
        void IJob.Execute()
        {
            Console.WriteLine("demo2:" + DateTime.Now);
        }
    }

}

