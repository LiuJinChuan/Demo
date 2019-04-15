using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Web.Data;
using Demo.Web.Model;
using Demo.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Demo.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //MVC服务
            services.AddMvc();
            //注册数据库服务
            services.AddDbContext<DataContext>(options => 
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
            //每次http请求都会生成一个WelcomeService实例
            //services.AddScoped<IWelcomeService, WelcomeService>();
            //添加自定义服务
            services.AddScoped<IServices<Student>, EFCoreServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
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
            //app.Use(next =>
            //{
            //    logger.LogInformation("app.Use()--------");
            //    return async httpContext =>
            //    {
            //        if (httpContext.Request.Path.StartsWithSegments("/first"))
            //        {
            //            await httpContext.Response.WriteAsync("First!");
            //        }
            //        else
            //        {
            //            await next(httpContext);
            //        }
            //    };
            //});
            //欢迎页面
            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //    Path = "/welcome"
            //});
            //静态文件的使用
            //app.UseFileServer();
            //app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();//默认mvc路由配置
            app.UseMvc(builder =>
            {
                builder.MapRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");
            });//按约定式配置路由
            //app.UseMvc();//在controller中配置路由
            //app.Run(async (context) =>
            //{
            //    //var welcome = configuration["Welcome"];
            //    var welcome = welcomeService.GetMessage();
            //    await context.Response.WriteAsync(welcome);
            //});
        }
    }
}
