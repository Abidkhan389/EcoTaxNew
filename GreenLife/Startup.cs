using GreenLife.AutoRapper;
using GreenLife.Data.EFCore;
using GreenLife.Models;
using GreenLife.Utilities.services;
using GreenLife.Utilities.Services;
using GreenLife.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddEntityFrameworkSqlServer();

            services.AddDbContextPool<GreenLifeFinalContext>(
            options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeDbConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequiredLength = 6;
                Options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<GreenLifeFinalContext>();
            services.AddScoped<EfCoreProductRepository>();
           services.AddScoped<EFCoreBrandRepository>();
           services.AddScoped<EFCoreCategoryRepository>(); 
           services.AddScoped<EFCoreCityRepository>(); 
           services.AddScoped<EFCoreBrandRepository>(); 
           services.AddScoped<EFCoreStoreRepository>(); 
           services.AddScoped<EFCoreStaffRepository>(); 
           services.AddScoped<EFCoreOrdersRepository>(); 
           services.AddScoped<EFCoreOrderItemsRepository>();
           services.AddScoped<EFCoreCustomerRepository>();
           services.AddScoped<EFCoreUserRepository>();
           services.AddScoped<EFCoreMessagesRepository>();
           services.AddScoped<IMailService, MailService>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));//for mail
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddHttpContextAccessor();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           // services.TryAddSingleton<ISession>();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly); // add services automapperprofile for auto object biding
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
