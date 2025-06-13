using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_Lab_11___12.Data;
using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Services.Interfaces;
using Projekt_Lab_11___12.Services;
using System;
namespace Projekt_Lab_11___12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Projekt_Lab_11___12ContextConnection") ?? throw new InvalidOperationException("Connection string 'Projekt_Lab_11___12ContextConnection' not found.");

            builder.Services.AddDbContext<Projekt_Lab_11___12Context>(options => options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
             .AddEntityFrameworkStores<Projekt_Lab_11___12Context>()
             .AddDefaultTokenProviders();


            builder.Services.AddScoped<IMineService, MineService>();
            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IEquipmentService, EquipmentService>();
            builder.Services.AddScoped<IUpgradeService, UpgradeService>();
            builder.Services.AddScoped<IRankingService, RankingService>();
            builder.Services.AddScoped<IUserStatisticsService, UserStatisticsService>();
            builder.Services.AddScoped<IAdminService, AdminService>();


            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();



            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Projekt_Lab_11___12Context>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                InitData.Initialize(context, roleManager, userManager);
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Mine}/{action=IronMine}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
