using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlServerBookStore.Data;
using SqlServerBookStore.Models;
using SqlServerBookStore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace SqlServerBookStore
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(p => {
                    p.Password.RequireDigit = false;
                    p.Password.RequireLowercase = false;
                    p.Password.RequireUppercase = false;
                    p.Password.RequireNonAlphanumeric = false;
                    p.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddAuthentication(o => {
            //    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme; })
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            //    {
            //        o.LoginPath = new PathString("/Home/Login/");
            //        o.LogoutPath = new PathString("/Home/Logout");
            //        o.AccessDeniedPath = new PathString("/Home/AccessDenied");
            //    }
            //);

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Home/Login";
                option.LogoutPath = "/Home/Logout";
                option.AccessDeniedPath = "/Home/AccessDenied";
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
