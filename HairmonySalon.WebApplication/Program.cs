﻿using Harmony.Repositories.Interfaces;
using Harmony.Services.Interfaces;
using HarmonySalon.Reponsitories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Harmony.Repositories;
using Harmony.Services;


public class Program
{
    private static string[]? args;

    public static void Main()
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container. ket noi csdl 
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<HarmonySalonContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        //cấu hình đăng kí dịch vụ identity 
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            // Các cấu hình mật khẩu khác của bạn
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredUniqueChars = 4;
        })
    .AddEntityFrameworkStores<HarmonySalonContext>()
    .AddDefaultTokenProviders();


        // dang ki dich vu
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
        // DI 
        /*builder.Services.AddDbContext<HarmonySalonContext>();*/
        // DI Repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        // DI Services
        builder.Services.AddScoped<IUserService, UserService>();
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

        app.UseAuthorization();
        /*// Route Account 
        app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");*/

        // Route hone 
        app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
        

    }

}