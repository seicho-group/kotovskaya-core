using Hangfire;
using Kotovskaya.Categories.Controllers;
using Kotovskaya.Shared.Application.ServiceConfiguration;

namespace Kotovskaya.Categories;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        new KotovskayaServicesConfiguration(services, typeof(Program).Assembly).Configure();
        services.AddHangfire(opt =>
        {
            opt.UseSqlServerStorage("Server=31.184.240.134,1433;User Id=sa;Password=Kotovskaya123;TrustServerCertificate=True");
        });
        services.AddHangfireServer();
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddCors(options =>
        {
            options.AddPolicy("*", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        services.AddControllers();
        services.AddSingleton<MsCategoriesController>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("*");

        app.UseHangfireDashboard("/dashboard");

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
