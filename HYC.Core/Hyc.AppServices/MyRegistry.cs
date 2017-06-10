using Hyc.AppServices.Demo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentScheduler;
using System.Reflection;
using Hyc.AppServices.HandlerAction;

namespace Hyc.AppServices
{
    public class MyRegistry : Registry
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

            // moduleSettings = moduleSettings.FindAll(c => c.IsOpen == true);

            AssemblyName assemblyName = new AssemblyName("Hyc.AppServices");
            foreach (var handler in moduleSettings)
            {
                Type type = Assembly.Load(assemblyName).GetType(handler.ActionName);
                IJob obj = (IJob)Activator.CreateInstance(type);
                if (obj == null)
                {
                    Console.WriteLine($"服务{handler.ActionName}不存在");
                    continue;
                }
                try
                {
                    IRunAction irun = new FactoryAction(handler.Interval.ToString()).Intit();
                    irun.Run(obj, handler);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"服务{handler.ActionName}出现异常:{ex.ToString()}");
                }
            }
        }
    }
}
