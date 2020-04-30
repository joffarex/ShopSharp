using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopSharp.Database;

namespace ShopSharp.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    context.Database.EnsureCreated();

                    if (!context.Users.Any())
                    {
                        var adminUser = new IdentityUser
                        {
                            UserName = "Admin"
                        };

                        var managerUser = new IdentityUser
                        {
                            UserName = "Manager"
                        };

                        userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                        userManager.CreateAsync(managerUser, "password").GetAwaiter().GetResult();

                        var adminClaim = new Claim("Role", "Admin");
                        var managerClaim = new Claim("Role", "Manager");

                        userManager.AddClaimAsync(adminUser, adminClaim).GetAwaiter().GetResult();
                        userManager.AddClaimAsync(managerUser, managerClaim).GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}