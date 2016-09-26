using System;
using System.Linq;
using System.Collections.Generic;
using MediatR;
using AlpineSkiHouse.Data;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Services;
using AlpineSkiHouse.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using AlpineSkiHouse.Configuration;

namespace AlpineSkiHouse
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = AlpineConfigurationBuilder.Build(env);
            CurrentEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }

        public IHostingEnvironment CurrentEnvironment {get ;}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddScoped<SingleInstanceFactory>(p => t => p.GetRequiredService(t));
            services.AddScoped<MultiInstanceFactory>(p => t => p.GetServices(t));

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddDbContext<ApplicationUserContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<SkiCardContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<PassContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<PassTypeContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ResortContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationUserContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IAuthorizationHandler, EditSkiCardAuthorizationHandler>();

            services.AddOptions();
            services.Configure<CsrInformationOptions>(Configuration.GetSection("CsrInformationOptions"));
            services.AddScoped<ICsrInformationService, CsrInformationService>();

            services.AddMvc();

            if (!CurrentEnvironment.IsDevelopment())
            {
                services.Configure<MvcOptions>(options =>
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                });
            }
            
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<ICardValidator, CardValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            if (Configuration["Authentication:Facebook:AppId"] == null ||
                Configuration["Authentication:Facebook:AppSecret"] == null ||
                Configuration["Authentication:Twitter:ConsumerKey"] == null ||
                Configuration["Authentication:Twitter:ConsumerSecret"] == null)
                throw new KeyNotFoundException("A configuration value is missing for authentication against Facebook and Twitter. While you don't need to get tokens for these you do need to set up your user secrets as described in the readme.");
            app.UseFacebookAuthentication(new FacebookOptions
            {
                AppId = Configuration["Authentication:Facebook:AppId"],
                AppSecret = Configuration["Authentication:Facebook:AppSecret"]
            });

            app.UseTwitterAuthentication(new TwitterOptions
            {
                ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"],
                ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"]
            });

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute("defaultArea", "Admin", "{area}/{controller}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
