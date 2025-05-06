using BookingService.Booking.Api;
using Serilog;

await CreateHostBuilder(args)
    .Build()
    .RunAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>

    Host.CreateDefaultBuilder(args)
         .ConfigureAppConfiguration((hostingContext, config) =>
             config.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables())
          
         .UseSerilog((hostingContext, config) =>
            config
            .WriteTo.Console()
            .ReadFrom.Configuration(hostingContext.Configuration))
         .ConfigureWebHostDefaults(webBuilder =>
         {
             webBuilder.UseStartup<Startup>();
         });
