using System;
using Microsoft.EntityFrameworkCore;
using TechJobsAuthentication.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
// var connectionString = builder.Configuration.GetConnectionString("JobDbContextConnection") ?? throw new InvalidOperationException("Connection string 'JobDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//--- MySql connection

//pomelo connection syntax: https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql
// working with the .NET 6 (specifically the lack of a Startup.cs)
//https://learn.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0#add-configuration-providers

var connectionString = builder.Configuration["ConnectionString"];
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddDbContext<JobDbContext>(dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion));

builder.Services.AddDefaultIdentity<IdentityUser>
(options => 
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 8;  
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<JobDbContext>();
//--- end of connection syntax


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
app.MapRazorPages();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

