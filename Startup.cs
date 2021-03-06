using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.Net
{
    public class Startup
    {
        public static string ContentRootPath{set;get;}
        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
           ContentRootPath = env.ContentRootPath; 
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages(); 
            // services.AddTransient<typeof(ILogger<>),typeof(Logger<>)>; => đã được inject vào 
            //Thietes lập razor engine
            services.Configure<RazorViewEngineOptions>(options =>{
                // /View/Controller/Action.cshtml
                // /MyView/Controller/Action.cshtml

                // {0} -> Tên action
                // {1} -> Tên controller
                // {2} -> Tên area
                options.ViewLocationFormats.Add("/MyViews/{1}/{0}"+RazorViewEngine.ViewExtension);
            });
            // services.AddSingleton<ProductService>();
            // services.AddSingleton<ProductService,ProductService>();
            // services.AddSingleton(typeof(ProductService));
            services.AddSingleton(typeof(ProductService),typeof(ProductService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Url : /{controller}/{action}/?{id}
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages(); 
            });

        }
    }
}
