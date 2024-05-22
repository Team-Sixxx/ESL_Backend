using ESLBackend.models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;


using Microsoft.AspNetCore.Authorization;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using ESLBackend.Utils;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
////builder.Services.AddDbContext<ApplicationDbContext>(options =>
////{
////    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
////    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
////});


//// Add services to the container.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    if (builder.Environment.IsDevelopment())
//    {
//        options.UseInMemoryDatabase("AppDb");
//    }
//    else
//    {
//        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//    }
//});


//builder.Services.AddScoped<DataSeeder>();

//var services = builder.Services;
//var configuration = builder.Configuration;


////builder.Services
////    .AddAuthentication(options =>
////    {
////        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
////        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
////    })
////    .AddCookie()
////    .AddGoogle(options =>
////    {
////        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
////        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
////        options.SaveTokens = true;
////    });


//builder.Services.AddAuthorization();

//builder.Services.AddDistributedMemoryCache();

//builder.Services.AddControllers();
////builder.Services.AddSession();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey
//    });


//});

//builder.Services.AddSession(options =>
//{
//    //options.Cookie.Name = ".ESLBackend.Session";
//    //options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust timeout as needed





//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//    options.Cookie.SameSite = SameSiteMode.None; // Set SameSite to None
//    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//    //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

//});



////options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
////options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;





//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
//{
//    // Configure Identity options if needed
//})
//.AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultTokenProviders();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
// .AddCookie(options =>
// {
//     //options.AccessDeniedPath = "/User/signin-google";
//     options.ExpireTimeSpan = TimeSpan.FromDays(1);
//     //options.LoginPath = "/User/signin-google";

//     options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
//     options.SlidingExpiration = true;
//     options.ExpireTimeSpan = TimeSpan.FromDays(1);


//     // Set SameSite mode to Lax

//     options.Cookie.SameSite = SameSiteMode.None;
//     options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
// })
//.AddGoogle(options =>
//{
//    //options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
//    //options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
//    //options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    //options.ReturnUrlParameter = "/User/google-signin-callback";
//    //options.CallbackPath = "/User/signin-google";
//    //options.SaveTokens = true;

//    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
//    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
//    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    //options.CallbackPath = "/User/google-signin-callback";
//    //options.CallbackPath = "/callback/signin-google";
//    options.SaveTokens = true;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))

//    };
//});



//var app = builder.Build();


//app.UseCookiePolicy(new CookiePolicyOptions()
//{
//    MinimumSameSitePolicy = SameSiteMode.None

//});

////app.MapIdentityApi<IdentityUser>();


////app.UseSession();
//if (app.Environment.IsDevelopment())
//{


//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI(o =>
//    {


//    });


//    using (var scope = app.Services.CreateScope())
//    {
//        var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
//        await dataSeeder.SeedTestUsersAsync();
//    }

//}

//app.UseHttpsRedirection();

//var cookiePolicyOptions = new CookiePolicyOptions
//{
//    MinimumSameSitePolicy = SameSiteMode.None,
//    //HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
//    //Secure = CookieSecurePolicy.Always,
//};



//app.UseRouting();



//app.UseCors(builder =>
//{
//    builder.AllowAnyOrigin()
//           .AllowAnyMethod()
//           .AllowAnyHeader();
//});


//app.UseCookiePolicy(cookiePolicyOptions);


//app.UseAuthentication(); // Add this line to enable authentication
//app.UseAuthorization();
//app.UseSession();



//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

//app.Run();



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
           options.UseMySql(connectionString, ServerVersion.AutoDetect((connectionString) + ";SslMode=None"));
        //options.UseInMemoryDatabase("AppDb");
    }
    else
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//});

builder.Services.AddScoped<DataSeeder>();

var services = builder.Services;
var configuration = builder.Configuration;

builder.Services.AddAuthorization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllers();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None; // Set SameSite to None
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure cookies are always secure
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Configure Identity options if needed
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.SaveTokens = true;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddHostedService<BookingCheckerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        await dataSeeder.SeedTestUsersAsync();
    }
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:5173") // Update with your React app's URL
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
});

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None
});

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
