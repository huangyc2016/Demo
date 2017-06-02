using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Hyc.Service;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using Hyc.Admin.Policy;
using Newtonsoft.Json.Serialization;

namespace Hyc.Admin
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
            AdminMapper.Initialize();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //依赖注入模块
            #region ===数据库底层操作依赖注入===
            services.AddSingleton<Repository.IMenuRepository, Repository.MenuRepository>();
            services.AddSingleton<Repository.IUserRepository, Repository.UserRepository>();
            services.AddSingleton<Repository.IRoleRepository, Repository.RoleRepository>();
            services.AddSingleton<Repository.IControllerRepository, Repository.ControllerRepository>();
            services.AddSingleton<Repository.IActionRepository, Repository.ActionRepository>();
            #endregion

            #region ===业务逻辑操作依赖注入===
            services.AddSingleton<Service.IMenuService, Service.MenuService>();
            services.AddSingleton<Service.IUserService, Service.UserService>();
            services.AddSingleton<Service.IRoleService, Service.RoleService>();
            services.AddSingleton<Service.IControllerService, Service.ControllerService>();
            services.AddSingleton<Service.IActionService, Service.ActionService>();
            #endregion

            //Add Session服务
            services.AddSession();

            //add AddAuthorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserOnly", policy => policy.RequireClaim("UserId"));
                options.AddPolicy("UserOnly1", policy => policy.RequireClaim("UserId1"));
                options.AddPolicy("Over21", policy => policy.Requirements.Add(new MinimumAgeRequirement(21)));
                options.AddPolicy("Passport", policy => policy.Requirements.Add(new PassportRequirement()));
            });

            //通过选项配置获取数据库连接信息
            services.AddOptions();
            services.Configure<MyOptions>(Configuration.GetSection("ConnectionStrings"));

            // Add framework services.
            services.AddMvc();

            //Add Newtonsoft.json
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //静态文件
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory())
            });

            //session
            app.UseSession();

            //cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "UserAuth",    // Cookie 验证方案名称，在写cookie时会用到。
                AutomaticAuthenticate = true,     // 是否自动启用验证，如果不启用，则即便客服端传输了Cookie信息，服务端也不会主动解析。除了明确配置了 [Authorize(ActiveAuthenticationSchemes = "上面的方案名")] 属性的地方，才会解析，此功能一般用在需要在同一应用中启用多种验证方案的时候。比如分Area.
                LoginPath = "/Login/Index",   // 登录页
                AccessDeniedPath = new PathString("/Login/Forbidden/"),//禁止访问页
                AutomaticChallenge = true     //

            });


            //默认路由
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
