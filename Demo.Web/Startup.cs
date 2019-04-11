using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Demo.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //每次http请求都会生成一个WelcomeService实例
            services.AddScoped<IWelcomeService, WelcomeService>();
            //MVC服务
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            IWelcomeService welcomeService, 
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                //开发者异常页面
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            //使用路由服务
            app.Use(next =>
            {
                logger.LogInformation("app.Use()--------");
                return async httpContext =>
                {
                    if (httpContext.Request.Path.StartsWithSegments("/first"))
                    {
                        await httpContext.Response.WriteAsync("First!");
                    }
                    else
                    {
                        await next(httpContext);
                    }
                };
            });
            //欢迎页面
            app.UseWelcomePage(new WelcomePageOptions
            {
                Path = "/welcome"
            });
            //静态文件的使用
            //app.UseFileServer();
            //app.UseDefaultFiles();
            app.UseStaticFiles();
            //配置默认mvc路由
            app.UseMvcWithDefaultRoute();
            app.Run(async (context) =>
            {
                //var welcome = configuration["Welcome"];
                var welcome = welcomeService.GetMessage();
                await context.Response.WriteAsync(welcome);
            });
        }
    }
}
