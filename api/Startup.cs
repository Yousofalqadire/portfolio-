using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using api.Profiles;
using api.Reposetory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext>(option=>
            {
                option.UseSqlite(Configuration.GetConnectionString("DefaultConniction"));
            });
            services.AddIdentity<ApplicationUser,IdentityRole>(Options=>{
                Options.Password.RequiredLength = 8;
                Options.Password.RequireUppercase = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireDigit = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddAuthentication(options=>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options=>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                   ValidateAudience = true,
                   ValidAudience = Configuration["AuthSetting:Audience"],
                   ValidIssuer = Configuration["AuthSetting:Issuer"],
                   RequireExpirationTime = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSetting:Key"])),
                   ValidateIssuerSigningKey = true
                };
            });
             services.AddCors();
             
            services.AddControllers().AddNewtonsoftJson(options=>
            {
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
             
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
            });
            services.Configure<FormOptions>(o=>{
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
             services.Configure<CloudenarySettings>(Configuration.GetSection("CloudinarySettings"));
             services.AddScoped<IPhoto,PhotoReposetory>();
             services.AddScoped<ITutorial,TutorialRepository>();
             services.AddAutoMapper(typeof(ProjectAutoMaper).Assembly);
             services.AddScoped<IHomeWork,HomeWorkRepository>();

            
        }

//     private void CheckSameSite(HttpContext httpContext, CookieOptions options)
// {
// 	if (options.SameSite == SameSiteMode.None)
// 	{
// 		var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
// 		if (userAgent ==  "someoldbroswer")
// 		{
// 			options.SameSite = SameSiteMode.Unspecified;
// 		}
// 	}
// }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

           app.UseCors(x=> x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"api",
                    pattern:"{controller}/{action}/{id?}"
                );
            });
        }

       
    }
}
