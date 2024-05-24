using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rocket_MyProject.DAL;
using Rocket_MyProject.Models;

namespace Rocket_MyProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<PortfolioContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddIdentity<AppUser, IdentityRole>(p=>
            {
                p.User.RequireUniqueEmail = true;
                p.Password.RequiredUniqueChars = 1;
                p.Password.RequireNonAlphanumeric = false;
                p.Password.RequireDigit = false;
                p.Password.RequireLowercase = false;
                p.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<PortfolioContext>()
            .AddDefaultTokenProviders();
            
             

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=portfolio}/{action=Index}/{id?}");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
