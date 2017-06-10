using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.AppServices.Demo
{
    public class Demo1 : IJob
    {
        void IJob.Execute()
        {
            System.Threading.Thread.Sleep(10000);
            Console.WriteLine("demo1:" + DateTime.Now);
        }
    }
}

