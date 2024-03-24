using AdminPanel.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Talabat.API.Helpers;
using Talabat.Core;
using Talabat.Core.Models.Identity;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace AdminPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //data base Contxet
            builder.Services.AddDbContext<StoreContext>(Options=>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("default Connection"));
            });

            //Identity Context //
            builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")); 
            });

            //AppUser Servisec

            builder.Services.AddIdentity<AppUser, IdentityRole>(Options =>
            {
                Options.Password.RequireUppercase = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireDigit = true;
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequiredLength  = 6;
            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.Services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));

            builder.Services.AddAutoMapper(typeof(MapProfile)); 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}"); 

            app.Run();
        }
    }
}