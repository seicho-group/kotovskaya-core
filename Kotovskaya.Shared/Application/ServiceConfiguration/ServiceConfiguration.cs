using System.Reflection;
using AutoMapper;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Shared.Application.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace Kotovskaya.Shared.Application.ServiceConfiguration;

public class KotovskayaServicesConfiguration(IServiceCollection services, Assembly assembly)
{
    public IServiceCollection Configure()
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "*",
                policy  =>
                {
                    policy.WithOrigins("http://example.com",
                        "http://www.contoso.com");
                });
        });
        DotNetEnv.Env.TraversePath().Load();

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ProductMapperProfile());
            mc.AddProfile(new CategoriesMapperProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
            
        services.AddSingleton<KotovskayaDbContext>();
        services.AddSingleton<KotovskayaMsContext>();
        
        return services;
    }

}
