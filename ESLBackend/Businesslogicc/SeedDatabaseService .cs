using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class SeedDatabaseService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public SeedDatabaseService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Check if the admin user exists
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                // Create the admin user if it doesn't exist
                var newAdminUser = new IdentityUser
                {
                    UserName = "Admin",
                    Email = "admin@example.com"
                };

                var result = await userManager.CreateAsync(newAdminUser, "12345678"); // Change the password to a secure one
                if (result.Succeeded)
                {
                    // Add the admin role to the admin user
                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
                }
                else
                {
                    throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors)}");
                }
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
