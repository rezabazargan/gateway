
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

new WebHostBuilder()
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config
            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
            .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json")
            .AddEnvironmentVariables();
    })
    .ConfigureServices(s => {
        s.AddOcelot();
    })
    .ConfigureLogging((hostingContext, logging) =>
    {
        //add your logging
        logging.AddConsole();
    })
    .UseIISIntegration()
    .Configure(app =>
    {
        app.UseOcelot().Wait();
    })
    .Build()
    .Run();
