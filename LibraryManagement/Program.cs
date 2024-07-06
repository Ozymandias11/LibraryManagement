using FluentValidation;
using Library.Data.Implementations;
using Library.Data.Interfaces;
using Library.Service;
using Library.Service.Interfaces;
using Library.Service.Logging;
using LibraryManagement;
using LibraryManagement.ActionFilters;
using LibraryManagement.ServiceExtensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using NLog;
using static System.Net.Mime.MediaTypeNames;
var builder = WebApplication.CreateBuilder(args);


//configuring logger service

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
"/nlog.config"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddScoped<IUserRoleRepository, UserRoleReposiotry>();
builder.Services.AddScoped<IDynamicMenuRepository, DynamicMenuRepository>();
builder.Services.AddScoped<IDynamicMenuService, DynamicMenuService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailTemplateReposiotry, EmailTemplateRepository>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISuperAdminUserService, SuperAdminUserService>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddMailjetConfiguration(builder.Configuration);
builder.Services.AddVonageConfiguration(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IVerificationCodeCacheService, VerificationCodeCacheService>();
builder.Services.AddScoped<ValidationFilterAttribute>();

builder.Services.ConfigureLoggerService();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Account/AccessDenied";
    });


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
app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
