using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PayPal.Core;
using PayPal.v1.Payments;
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

            ////last
            //services.AddSingleton<IPaypalServices, PaypalServices>();
            //services.Configure<PayPalAuthOptions>(Configuration);


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

            //app.Run(async (context) =>
            //{
            //    var environment = new SandboxEnvironment("(Ae-cbaOklKc8zb09tgLm8W6ZqrbsVugp5wF-xS2_heZr7_vtYoZFhF21FcOT1QC4VOBK7BWoxhxas6L8)", "(EIr9HxvU87p9Fmt20AfW844KRw7GI71Jk5IWQEqC6AJ590-UZKe7KJwOQMINnXXBdWX0u2_MnqYnjQpk)");
            //    var client = new PayPalHttpClient(environment);

            //    var payment = new PayPal.v1.Payments.Payment()
            //    {
            //        Intent = "sale",
            //        Transactions = new List<PayPal.v1.Payments.Transaction>()
            //        {
            //            new PayPal.v1.Payments.Transaction()
            //            {
            //                Amount = new Amount()
            //                {
            //                    Total = "10",
            //                    Currency = "USD"
            //                }
            //            }
            //        },
            //        RedirectUrls = new RedirectUrls()
            //        {
            //            ReturnUrl = "https://www.example.com/",
            //            CancelUrl = "https://www.example.com"
            //        },
            //        Payer = new Payer()
            //        {
            //            PaymentMethod = "paypal"
            //        }
            //    };

            //    PaymentCreateRequest request = new PaymentCreateRequest();
            //    request.RequestBody(payment);

            //    System.Net.HttpStatusCode statusCode;

            //    try
            //    {
            //        BraintreeHttp.HttpResponse response = await client.Execute(request);
            //        statusCode = response.StatusCode;
            //        Payment result = response.Result<Payment>();

            //        string redirectUrl = null;
            //        foreach (LinkDescriptionObject link in result.Links)
            //        {
            //            if (link.Rel.Equals("approval_url"))
            //            {
            //                redirectUrl = link.Href;
            //            }
            //        }

            //        if (redirectUrl == null)
            //        {
            //            // Didn't find an approval_url in response.Links
            //            await context.Response.WriteAsync("Failed to find an approval_url in the response!");
            //        }
            //        else
            //        {
            //            await context.Response.WriteAsync("Now <a href=\"" + redirectUrl + "\">go to PayPal to approve the payment</a>.");
            //        }
            //    }
            //    catch (BraintreeHttp.HttpException ex)
            //    {
            //        statusCode = ex.StatusCode;
            //        var debugId = ex.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
            //        await context.Response.WriteAsync("Request failed!  HTTP response code was " + statusCode + ", debug ID was " + debugId);
            //    }
            //});

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
