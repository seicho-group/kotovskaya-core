namespace Kotovskaya.API;

public class Program
{
    public static void Main(string[] args)
    {
        DotNetEnv.Env.Load();
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .ConfigureAppConfiguration(
                (hostingContext, config) =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile($"Ocelot.{env}.json", false, true);
                });
    }
}
