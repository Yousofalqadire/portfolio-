using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

 namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Refliction> Reflictions { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }



        public ApplicationDbContext(DbContextOptions options):base(options)
        {
        }
         protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    }
}