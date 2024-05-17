using Kotovskaya.Categories.Controllers;
using Kotovskaya.Shared.Application.ServiceConfiguration;

namespace Kotovskaya.Categories
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            new KotovskayaServicesConfiguration(services, typeof(Program).Assembly).Configure();
            
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("*");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
