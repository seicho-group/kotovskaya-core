using AutoMapper;
using Kotovskaya.Categories.Controllers;
using Kotovskaya.Categories.Domain.DTO;
using Kotovskaya.DB.Domain.Context;

namespace Kotovskaya.Categories
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // automapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CategoriesMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            // database
            services.AddSingleton<KotovskayaDbContext>();
            
            // redis
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Environment.GetEnvironmentVariable("PG_HOST");
                options.InstanceName = "kot-redis";
            });
            
            services.AddControllers();
            services.AddSingleton<MsCategoriesController>();
            services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
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