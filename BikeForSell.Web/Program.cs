using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BikeForSell.Infrastructure;
using BikeForSell.Application;
using BikeForSell.Application.Interfaces;
using BikeForSell.Application.Services;
using BikeForSell.Domain.Interfaces;
using BikeForSell.Infrastructure.Repositories;
using BikeForSell.Domain.Models;

namespace BikeForSell.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Context>();
            builder.Services.AddControllersWithViews();

            //Injection from Infrastructure
            builder.Services.AddInfrastructure();

            //Injection from Appliacation
            builder.Services.AddApplication();

            builder.Services.Configure<IdentityOptions>(options => 
            { 
                options.SignIn.RequireConfirmedAccount = false;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}