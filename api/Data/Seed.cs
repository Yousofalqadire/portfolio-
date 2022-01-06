using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class Seed
    {
        public static async Task SeedReflictions(ApplicationDbContext db)
        {
            if( await db.Reflictions.AnyAsync()) return;
            var reflictionsDataSource = await System.IO.File.ReadAllTextAsync("Data/seedData/reflictions.json");
            var reflictions = JsonSerializer.Deserialize<List<Refliction>>(reflictionsDataSource); 
            await db.AddRangeAsync(reflictions);
            await db.SaveChangesAsync();
        }
        public static async Task SeedSkills(ApplicationDbContext db)
        {
            if(await db.Skills.AnyAsync()) return;
            var skillsDataSource = await System.IO.File.ReadAllTextAsync("Data/seedData/skills.json");
            var skills = JsonSerializer.Deserialize<List<Skill>>(skillsDataSource);
            await db.AddRangeAsync(skills);
            await db.SaveChangesAsync();
        }
         public static async Task SeedTutorials(ApplicationDbContext db)
        {
            if(await db.Tutorials.AnyAsync()) return;
            var tutorialsDataSource = await System.IO.File.ReadAllTextAsync("Data/seedData/tutorial.json");
            var tutorial = JsonSerializer.Deserialize<Tutorial>(tutorialsDataSource);
            await db.AddAsync(tutorial);
            await db.SaveChangesAsync();
        }
         public static async Task SeedAdmin(UserManager<ApplicationUser> userManager,
                                           RoleManager<IdentityRole> roleManager)
        {
            if( !userManager.Users.Any()){   
            ApplicationUser Admin = new ApplicationUser
            {
                UserName = "qadire333@yahoo.com",
               FirstName = "yousof",
               LastName = "qadire",
               Email = "qadire333@yahoo.com",
            };
             await userManager.CreateAsync(Admin,"Yousf@qa#1");
              IdentityRole role = new IdentityRole
           {
               Name = "Admin"
           };
           await roleManager.CreateAsync(role);
             await userManager.AddToRoleAsync(Admin, role.Name);
            }         
        }

        public static async Task SeedHomeWorks(ApplicationDbContext db)
        {
            if( await db.HomeWorks.AnyAsync()) return;
            var homeWorksDataSource = await System.IO.File.ReadAllTextAsync("Data/seedData/homeworks.json");
            var homeWorks = JsonSerializer.Deserialize<List<HomeWork>>(homeWorksDataSource);
            await  db.HomeWorks.AddRangeAsync(homeWorks);
            await db.SaveChangesAsync();
        }
    }
}