using Hyc.App.Actions;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hyc.App.System.ActionHandle
{
    public class HandlerActions
    {
        private IConfigurationRoot _configuration;
        private static List<AutoResetEvent> _WaitCallbackList;
        public HandlerActions()
        {
            //设置config
            var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            //线程等待列表
            _WaitCallbackList = new List<AutoResetEvent>();
        }
        public void Init()
        {
            var moduleSettings = new List<HandlerOptions>();
            _configuration.GetSection("HandlerAction").Bind(moduleSettings);
            List<Task> tasks = new List<Task>();
            TaskFactory taskFactory = new TaskFactory();

            foreach (var item in moduleSettings)
            {
                Task t = taskFactory.StartNew(() =>
                {
                    RunAction(item);
                });
                tasks.Add(t);
                Console.WriteLine($"Handler Name is {item.Name},Minute is {item.Minute}");
            }
            //任何提供的任务已完成时创建将完成的任务
            Task.WhenAny(tasks.ToArray());
        }

        public void RunAction(HandlerOptions handler)
        {
            string assemblyPath = AppContext.BaseDirectory + @"\Hyc.App.Actions.dll";
            string Name = handler.Name;
            string asmName = handler.Handler;
            var asl = new AssemblyLoader(asmName);
            var asm = asl.LoadFromAssemblyPath(@assemblyPath);
            var type = asm.GetType(handler.Handler);
            dynamic obj = Activator.CreateInstance(type);
            obj.Execute();
        }

        public void RunAction1(HandlerOptions handler)
        {
            string assemblyPath = AppContext.BaseDirectory + @"\Hyc.App.Actions.dll";
            string Name = handler.Name;
            string asmName = handler.Handler;
            var asl = new AssemblyLoader(asmName);
            var asm = asl.LoadFromAssemblyPath(@assemblyPath);
            var type = asm.GetType(handler.Handler);
            dynamic obj = Activator.CreateInstance(type);
            obj.Execute();
        }
    }
}
