﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Hyc.Service;
using Hyc.Repository;

namespace Hyc.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            //初始化映射
            ApiMapper.Initialize();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注入配置文件
            services.AddSingleton(Configuration);
            //依赖注入模块
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserService, UserService>();

            //通过选项配置
            services.AddOptions();
            services.Configure<MyOptions>(myoptions =>
            {
                myoptions.sqlConnectionHyc = "Data Source=LAPTOP-5GQAOH98;Initial Catalog=LeoDb;User ID=sa;Password=123456;";
                myoptions.sqlConnectionLeo = "Data Source=LAPTOP-5GQAOH98;Initial Catalog=LeoDb;User ID=sa;Password=123456;";
            });

            //添加swagger
            services.AddSwaggerGen();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //添加swagger
            app.UseSwagger();
            app.UseSwaggerUi();

            app.UseMvc();
        }
    }
}
