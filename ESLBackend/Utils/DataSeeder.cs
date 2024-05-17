using ESLBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



    public class DataSeeder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public DataSeeder(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public async Task SeedTestUsersAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var testUsers = _configuration.GetSection("TestUsers").Get<List<TestUser>>();

            foreach (var testUser in testUsers)
            {
                if (await context.Users.AnyAsync(u => u.Email == testUser.Email) == false)
                {
                    var user = new User
                    {
                        Email = testUser.Email,
                        UserName = testUser.Email,
                        Enable = true 
                    };
                    context.Users.Add(user);
                }
            }

            await context.SaveChangesAsync();
        }
  


    public class TestUser
    {
        public string Email { get; set; }
    
    }
}
