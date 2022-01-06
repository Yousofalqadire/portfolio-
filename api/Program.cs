using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           var host =  CreateHostBuilder(args).Build();
           using var scope = host.Services.CreateScope();
           var service = scope.ServiceProvider;
           try
           {
               var context = service.GetRequiredService<ApplicationDbContext>();
                var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
             var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
                await context.Database.MigrateAsync();
                await Seed.SeedReflictions(context);
                await Seed.SeedSkills(context);
                await Seed.SeedTutorials(context);
                await Seed.SeedAdmin(userManager,roleManager);
                await Seed.SeedHomeWorks(context);
           }catch(Exception ex)
           {
               var logger = service.GetRequiredService<ILogger<Program>>();
               logger.LogError(ex,"An Error accured durring migration ");
           }
           await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
