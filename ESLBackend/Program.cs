using ESLBackend.models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ESLBackend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

/*
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseInMemoryDatabase("AppDb");
    }
    else
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});
*/


//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
//{
//    // Configure Identity options if needed
//})
//.AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultTokenProviders();

//builder.Services.AddScoped<ITokenService, JwtTokenService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ESLBackend", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// Retrieve the configuration
var configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"], // Your issuer from appsettings.json
            ValidAudience = configuration["Jwt:Audience"], // Your audience from appsettings.json
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])) // Your secret key from appsettings.json
        };
    });


//using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
//{
//    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
//    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//    // Check if the admin role exists
//    var adminRole = await roleManager.FindByNameAsync("Admin");
//    if (adminRole == null)
//    {
//        // Create the admin role if it doesn't exist
//        adminRole = new IdentityRole("Admin");
//        await roleManager.CreateAsync(adminRole);
//    }

//    // Check if the admin user exists
//    var adminUser = await userManager.FindByEmailAsync("admin@example.com");
//    if (adminUser == null)
//    {
//        // Create the default admin user
//        adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com" };
//        await userManager.CreateAsync(adminUser, "YourStrongPassword");

//        // Assign admin role to the user
//        await userManager.AddToRoleAsync(adminUser, "Admin");
//    }
//}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ESLBackend"));
}

app.UseHttpsRedirection();

app.UseRouting();


app.UseAuthentication(); // Add this line to enable authentication
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
