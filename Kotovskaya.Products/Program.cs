namespace Kotovskaya.Products;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseSentry(o =>
                {
                    o.Dsn =
                        "https://3b96c147b972abc247a3d332ca9f0675@o4507357425303552.ingest.de.sentry.io/4507357435461712";
                    o.TracesSampleRate = 1.0;
                    o.ProfilesSampleRate = 1.0;
                });
            }).ConfigureAppConfiguration(
                (hostingContext, config) => { config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath); });
    }
}
