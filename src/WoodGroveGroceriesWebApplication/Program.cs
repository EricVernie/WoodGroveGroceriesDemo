namespace WoodGroveGroceriesWebApplication
{
    using EntityFramework;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Models.Settings;
    using Services;

    public class Program
    {

        public static void CMain(string[] args)
        {
            var webHost = CreateDebugHostBuilder(args).Build();
            using (var serviceScope = webHost.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<WoodGroveGroceriesDbContext>();
                var dbContextInitializationOptions = serviceProvider.GetRequiredService<IOptions<DbContextInitializationOptions>>().Value;
                var host = serviceProvider.GetRequiredService<HostService>();

                WoodGroveGroceriesDbContextInitializer.InitializeAsync(dbContext, dbContextInitializationOptions, host)
                    .Wait();
            }

            webHost.Run();
        }

        public static IHostBuilder CreateDebugHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<DebugStartup>();
                });

        public static void Main(string[] args)
        {
            //var webHost = BuildWebHost(args);
            var webHost = CreateHostBuilder(args);

            using (var serviceScope = webHost.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<WoodGroveGroceriesDbContext>();
                var dbContextInitializationOptions = serviceProvider.GetRequiredService<IOptions<DbContextInitializationOptions>>().Value;
                var host = serviceProvider.GetRequiredService<HostService>();

                WoodGroveGroceriesDbContextInitializer.InitializeAsync(dbContext, dbContextInitializationOptions, host)
                    .Wait();
            }

            webHost.Run();
        }

        public static IHost CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).Build();

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}