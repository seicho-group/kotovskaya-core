using Kotovskaya.Products.Controllers;
using Kotovskaya.Shared.Application.ServiceConfiguration;

namespace Kotovskaya.Products;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        new KotovskayaServicesConfiguration(services, typeof(Program).Assembly).Configure();
        services.AddCors(options =>
        {
            options.AddPolicy("*", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddControllers();
        services.AddSingleton<ProductsController>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCors("*");

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}