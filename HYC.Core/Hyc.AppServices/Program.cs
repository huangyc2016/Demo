using FluentScheduler;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;

namespace Hyc.AppServices
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("start the app...");

            //注册管理各个服务
            JobManager.Initialize(new MyRegistry());
            Console.ReadLine();
        }
    }
}