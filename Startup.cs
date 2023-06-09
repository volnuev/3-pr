using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using shop.Data.Models;
using shop.Data.Interfaces;
using shop.Data.DataBase;
using shop.Data.Common;
using System.Web;
using shop.Data.ViewModell;

namespace shop
{
    public class Startup
    {
        public static List<ItemBasket> BasketItem = new List<ItemBasket>();
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICategorys, DBCategory>();
            services.AddTransient<IItems, DBItems>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddHttpContextAccessor(); // Добавляем сервис IHttpContextAccessor
            services.AddSession(); // Добавляем сервис сессии
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession(); // Используем сессию
            app.UseMvcWithDefaultRoute();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

            });
        }
    }
}
