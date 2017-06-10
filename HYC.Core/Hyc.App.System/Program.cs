using FluentScheduler;
using Hyc.App.Actions;
using Hyc.App.System.ActionHandle;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Hyc.App.System
{
    class Program
    {
        
        static void Main(string[] args)
        {

            //IServiceCollection services = new ServiceCollection();

            ////注入
            //services.AddSingleton<Actions.Demo.IDemo1, Actions.Demo.Demo1>();
            //services.AddSingleton<Actions.Demo.IDemo2, Actions.Demo.Demo2>();
            ////构建容器
            //IServiceProvider serviceProvider = services.BuildServiceProvider();
            ////解析
            //var demo1 = serviceProvider.GetService<Actions.Demo.IDemo1>();
            //var demo2 = serviceProvider.GetService<Actions.Demo.IDemo2>();
            //demo1.Run();
            //demo2.Run();
            ////Console.WriteLine(memcachedClient);


            ////Console.WriteLine("Hello World!");
            //Console.ReadKey();


            //HandlerActions handler = new HandlerActions();
            //handler.Init();

            JobManager.Initialize(new MyRegistry());

            Console.ReadLine();
        }
    }
}