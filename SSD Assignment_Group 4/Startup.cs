using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment_Group_4.Models;
using SSD_Assignment_Group_4.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using SSD_Assignment_Group_4.Services;

namespace SSD_Assignment_Group_4
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
            services.AddRazorPages();

            services.AddDbContext<SSD_Assignment_Group_4Context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SSD_Assignment_Group_4Context")));

            services.AddIdentity<RecipeUser, ApplicationRole>((config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            }))
                .AddDefaultUI()
                .AddEntityFrameworkStores<SSD_Assignment_Group_4Context>()
                .AddDefaultTokenProviders();


            services.AddMvc()
            .AddRazorPagesOptions(options =>
            {
                 // options.Conventions.AllowAnonymousToFolder("/Recipes");
                 // options.Conventions.AuthorizePage("/Recipes/Create");
                 // options.Conventions.AuthorizeAreaPage("Identity", "/Manage/Accounts");
                 options.Conventions.AuthorizeFolder("/Recipes");
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            })
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // options.Cookie.Name = "YourCookieName";
                //  options.Cookie.Domain=
                // options.LoginPath = "/Account/Login";
                // options.LogoutPath = "/Account/Logout";
                //options.AccessDeniedPath = "/Account/AccessDenied";

                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromSeconds(80);
                options.SlidingExpiration = true;

            });

            //services.AddDefaultIdentity<RecipeUser>(config =>
            //{
            //    config.SignIn.RequireConfirmedEmail = true;
            //})
                //.AddEntityFrameworkStores<SSD_Assignment_Group_4Context>();

            //requires
            // using Microsoft.AspNetCore.Identity.UI.Services;
            //using WebPWrecover.Services;
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            UserManager<RecipeUser> _userManager,
            RoleManager<ApplicationRole> _roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePages("text/html", "<h1>Status code page</h1> <h2>Status Code: {0}</h2>");
                app.UseExceptionHandler("/Error");
            }



            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            SeedData.SeedUsers_Roles(_userManager, _roleManager);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
