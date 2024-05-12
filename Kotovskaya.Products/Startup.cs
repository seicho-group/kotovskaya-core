using AutoMapper;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Products.Controllers;
using Kotovskaya.Products.Domain;

namespace Kotovskaya.Products
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
                mc.AddProfile(new ProductMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllers();
            services.AddSingleton<ProductsController>();
            services.AddSingleton<KotovskayaDbContext>();
            services.AddSingleton<KotovskayaMsContext>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}