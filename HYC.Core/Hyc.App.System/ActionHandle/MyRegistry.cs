using FluentScheduler;
using Hyc.App.Actions;
using Hyc.App.Actions.Demo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Hyc.App.System.ActionHandle
{
    public class MyRegistry:Registry
    {
        private IConfigurationRoot _configuration;
        public MyRegistry()
        {

            //设置config
            var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
            var moduleSettings = new List<HandlerOptions>();
            _configuration.GetSection("HandlerAction").Bind(moduleSettings);

            foreach (var item in moduleSettings)
            {
                //HandlerOptions handler = item;

                //string assemblyPath = AppContext.BaseDirectory + @"\Hyc.App.Actions.dll";
                //string Name = handler.Name;
                //string asmName = handler.Handler;
                //var asl = new AssemblyLoader(asmName);
                //var asm = asl.LoadFromAssemblyPath(@assemblyPath);
                //var type = asm.GetType(handler.Handler);
                //PropertyInfo property = type.GetProperty("MyJob");
                //string s = "MyJob";
                //var obj = Activator.CreateInstance(type);
                Schedule<MyJob>().ToRunNow().AndEvery(2).Seconds();
                Console.WriteLine($"Handler Name is {item.Name},Minute is {item.Minute}");
            }
        }
    }

    public class MyJob : IJob
    {

        void IJob.Execute()
        {
            Console.WriteLine("现在时间是：" + DateTime.Now);
        }
    }
}
