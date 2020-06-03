using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WoodGroveGroceriesWebApplication.Entities;
using WoodGroveGroceriesWebApplication.EntityFramework;
using WoodGroveGroceriesWebApplication.Managers;
using WoodGroveGroceriesWebApplication.Models.Settings;
using WoodGroveGroceriesWebApplication.Repositories;
using WoodGroveGroceriesWebApplication.Services;
using WoodGroveGroceriesWebApplication.ViewServices;

namespace WoodGroveGroceriesWebApplication
{
    public class DebugStartup
    {
        public DebugStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            ConfigureOptions(Configuration, services);
            ConfigureViewServices(services);
            ConfigureManagers(services);            
            ConfigureRepositories(services);
            ConfigureStores(services);
            services.AddSingleton<HostService, HostService>();

        }
        private void ConfigureOptions(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IndustryManager, IndustryManager>();
            services.Configure<DbContextInitializationOptions>(configuration.GetSection("DbContextInitialization"));
        }
        private void ConfigureManagers(IServiceCollection services)
        {
            services.AddTransient<ICatalogItemManager, CatalogItemManager>();
            services.AddTransient<IPantryManager, PantryManager>();
            services.AddTransient<ITrolleyManager, TrolleyManager>();
        }
        private void ConfigureViewServices(IServiceCollection services)
        {
            services.AddTransient<ICatalogItemViewService, CatalogItemViewService>();
            services.AddTransient<IPantryViewService, PantryViewService>();
            services.AddTransient<ITrolleyViewService, TrolleyViewService>();
        }
        private void ConfigureStores(IServiceCollection services)
        {
            services.AddDbContext<WoodGroveGroceriesDbContext>(options => { options.UseInMemoryDatabase("WoodGroveGroceries"); },
                ServiceLifetime.Singleton);
        }
        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<ICatalogItemRepository, CatalogItemDbRepository>();
            services.AddScoped<IRepository<CatalogItem>, DbRepository<CatalogItem>>();
            services.AddScoped<IRepository<Pantry>, DbRepository<Pantry>>();
            services.AddScoped<IRepository<Trolley>, DbRepository<Trolley>>();
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

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=DebugHome}/{action=Index}/{id?}");
            });
        }
    }
}
