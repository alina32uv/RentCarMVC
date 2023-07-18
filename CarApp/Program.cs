using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarApp.Data;
using Microsoft.AspNetCore.Identity;
using CarApp.Interfaces;
using CarApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using CarApp.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CarAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarAppContext") ?? throw new InvalidOperationException("Connection string 'CarAppContext' not found.")));
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarAppContext>();



builder.Services.AddScoped<ICarBrand, BrandRepo>();
builder.Services.AddScoped<ITransmission, TransmissionRepo>();
builder.Services.AddScoped<IStatus, StatusRepo>();
builder.Services.AddScoped<IVehicle, VehicleRepo>();
builder.Services.AddScoped<IDrive, DriveRepo>();
builder.Services.AddScoped<IFuel, FuelRepo>();
builder.Services.AddScoped<ICarView, CarViewRepo>();
builder.Services.AddScoped<IInsurance, InsuranceRepo>();

builder.Services.AddScoped<IBody, CarBodyTypeRepo>();
builder.Services.AddScoped<ICar, CarRepo>();
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
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
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Manager", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

    }
}


    app.Run();
