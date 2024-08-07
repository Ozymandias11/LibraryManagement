using AspNetCoreHero.ToastNotification;
using FluentValidation;
using Library.Data.Implementations;
using Library.Data.Interfaces;
using Library.Service;
using Library.Service.Interfaces;
using Library.Service.Library.Implementations;
using Library.Service.Library.Interfaces;
using Library.Service.Logging;
using LibraryManagement;
using LibraryManagement.ActionFilters;
using LibraryManagement.ServiceExtensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using NLog;
using System.Globalization;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
var builder = WebApplication.CreateBuilder(args);


//configuring logger service

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
"/nlog.config"));



builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assemblyName = new AssemblyName(typeof(SharedResource).Assembly.FullName!);
            return factory.Create(nameof(SharedResource), assemblyName.Name!);
        };
                             
    });


builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.AddAutoMapper(typeof(Program));
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
builder.Services.AddScoped<IResultHandlerService, ResultHandlerService>();    

builder.Services.ConfigureLoggerService();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddNotyf(config => {
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.BottomRight;
});

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-GB"),
        new CultureInfo("de-DE")
    };

    options.DefaultRequestCulture = new RequestCulture("de-DE");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});


var app = builder.Build();


var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions.Value);

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
