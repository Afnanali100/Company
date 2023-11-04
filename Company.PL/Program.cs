using Company.BLL.Interfaces;
using Company.BLL.Repositrios;
using Company.DAL.Contexts;
using Company.DAL.Models;
using Company.PL.MapperProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configue Services That Allow depndancy Injection
            builder.Services.AddControllersWithViews();


            //DI
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection"));
            });


            builder.Services.AddControllersWithViews();
            //  services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            // services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
            // services.AddTransient<DepartmentRepository, DepartmentRepository>();
            //  services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            //  services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new DepartmentProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new UserProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new RoleProfile()));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //         services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityUser>>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;

            }).AddEntityFrameworkStores<CompanyDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(options => {
                    options.LoginPath = "Account/Login";
                    options.AccessDeniedPath = "Home/Error";

                });
            #endregion
            var app = builder.Build();

            var env = builder.Environment;
            #region Configure Http Request Pipline
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



            #endregion


            app.Run();
        }

       
    }
}
