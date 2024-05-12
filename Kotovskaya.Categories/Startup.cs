using AutoMapper;
using Kotovskaya.Categories.Controllers;
using Kotovskaya.Categories.Domain.DTO;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Products.Domain;

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
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CategoriesMapperProfile());
                mc.AddProfile(new ProductMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddSingleton<KotovskayaDbContext>();
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