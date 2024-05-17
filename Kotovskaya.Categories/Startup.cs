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
            // redis
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Environment.GetEnvironmentVariable("PG_HOST");
                options.InstanceName = "kot-redis";
            });
          
            services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            services.AddCors(options => options.AddPolicy("policy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));

            services.AddControllers();
            services.AddSingleton<MsCategoriesController>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
