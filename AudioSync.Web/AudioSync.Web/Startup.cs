using AudioSync.API.DependencyInjection;
using AudioSync.Repository.DbContexts;
using AudioSync.Web.Hubs;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Net.WebSockets;

namespace AudioSync.Web
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
            services.RegisterAllDependencies(Configuration);
            services.AddDbContext<AudioSyncDbContext>(options =>
            {
                options.UseSqlServer(Configuration["DataConnections:ConnectionString_Core"]);
            }, ServiceLifetime.Scoped);

            services.AddDbContextFactory<AudioSyncDbContext>(options =>
            {
                options.UseSqlServer(Configuration["DataConnections:ConnectionString_Core"]);
            }, ServiceLifetime.Scoped);
            services.AddControllersWithViews();

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration["DataConnections:ConnectionString_Core"]));
            services.AddHangfireServer();
            services.AddSignalR();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireDashboard("/hangfire");
            
            
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromMinutes(10),
            };
            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ConnectionHub>("/connectionHub");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
