using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Transofast.Application.Extensions;
using Transofast.Domain.Entities.Concrete;
using Transofast.Infrastructure.DataAccess;
using Transofast.Infrastructure.Extensions;
using Transofast.Application.AutoMapper;
using Transofast.Presentation.MapperProfile;
using Microsoft.AspNetCore.Builder;
using Transofast.Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddBusinessServices();
builder.Services.AddRepositoryServices();
builder.Services.AddDbContext<TransofastDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection")));

builder.Services.AddAutoMapper(typeof(Mapping), typeof(MappingUI));
builder.Services.AddSignalR();


builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "qwertyuopasdfghjklizxcvbnm1234567890!@#$%&*()_-+=<>?QWERTYUOPASDFGHJKLIZXCVBNM";

    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = false;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    options.Lockout.MaxFailedAccessAttempts = 3;

}).AddDefaultTokenProviders()
  .AddEntityFrameworkStores<TransofastDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//builder.Services.Configure<SecurityStampValidatorOptions>(options =>
//{
//    options.ValidationInterval = TimeSpan.FromSeconds(1200); // Oturum süresi 30 dakika
//});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chatHub");
});

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
