using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrenditiDaBere.Data;
using PrenditiDaBere.Data.Interfaces;
using PrenditiDaBere.Data.mocks;
using PrenditiDaBere.Data.Models;
using PrenditiDaBere.Data.Repositories;

namespace PrenditiDaBere
{
    public class Startup
    {
    //    //private IConfigurationRoot _configurationRoot;

    //    //0
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //public Startup(IHostingEnvironment hostingEnvironment)
        //{
        //    _configurationRoot = new ConfigurationBuilder()
        //        .SetBasePath(hostingEnvironment.ContentRootPath)
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //}

        public void ConfigureServices(IServiceCollection services)
        {
            // Server configuration
            services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            // If you want to tweak Identity cookies, they're no longer part of IdentityOptions.
            //services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");
            //services.AddAuthentication()
            //        .AddFacebook(options =>
            //        {
            //            options.AppId = Configuration["auth:facebook:appid"];
            //            options.AppSecret = Configuration["auth:facebook:appsecret"];
            //        });

            //new latest Authentication, Identity config
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();


            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IBibitaRepository, BibitaRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShoppingCart.GetCart(sp));
            services.AddTransient<IOrdineRepository, OrdineRepository>();

            //1
            services.AddMvc();


            services.AddMemoryCache();
            services.AddSession();
        }

        //    //
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            //this.Configuration.GetConnectionString(LoggerMessage);
            ////last add
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "drinkdetails",
                   template: "Drink/Details/{drinkId?}",
                   defaults: new { Controller = "Drink", action = "Details" });

                routes.MapRoute(
                    name: "categoryfilter",
                    template: "Drink/{action}/{category?}",
                    defaults: new { Controller = "Drink", action = "List" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{Id?}");
            });

            //DbInitializer.Seed(app); qui no !!!
        }
    }
}
