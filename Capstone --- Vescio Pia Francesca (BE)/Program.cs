using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Password_Crypth_Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Capstone_____Vescio_Pia_Francesca__BE_.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Capstone_____Vescio_Pia_Francesca__BE_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Capstone_____Vescio_Pia_Francesca__BE_Context") ?? throw new InvalidOperationException("Connection string 'Capstone_____Vescio_Pia_Francesca__BE_Context' not found.")));




// Add services to the container.
builder.Services.AddControllersWithViews();

var conn = builder.Configuration.GetConnectionString("SqlServer")!;
builder.Services
    .AddDbContext<DataContext>(opt => opt.UseSqlServer(conn))
    ;

builder.Services
    .AddScoped<IRacesService, RaceService>()
    .AddScoped<INationService, NationService>()
    .AddScoped<ICitiesService, CitiesService>()
    .AddScoped<IEcoService, EcoService>()
    .AddScoped<IGuildService, GuildService>()
    .AddScoped<IEventService, EventService>()
    .AddScoped<IRoleService, RoleService>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<IPasswordEncoder, PasswordEncoder>()
    .AddScoped<IArticleService, ArticleService>()
    ;
string key = builder.Configuration["Jwt:Key"]!;
var bytesKey = System.Text.Encoding.UTF8.GetBytes(key);

builder.Services.AddAuthentication( opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(opt => opt.LoginPath = "/Auth/Login")
    .AddJwtBearer( opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(bytesKey)
        };
    })
    ;
builder.Services.AddAuthorization();
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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
