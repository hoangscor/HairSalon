using Harmony.Repositories.Interfaces;
using Harmony.Repositories;
using Harmony.Services.Interfaces;
using Harmony.Services;
using Harmony.WebApplication.Data;
using HarmonySalon.Reponsitories.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Kết nối CSDL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
	?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<HarmonySalonContext>(options =>
	options.UseSqlServer(connectionString));

// Thêm Razor Pages
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Thêm các dịch vụ khác như Identity, các service custom, v.v.
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<HarmonySalonContext>();

var app = builder.Build();

// Cấu hình HTTP request pipeline
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Cấu hình route
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Chạy ứng dụng
app.Run();
