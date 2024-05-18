using System.Reflection;
using AspNetCore.Yandex.ObjectStorage.Extensions;
using AutoMapper;
using DotNetEnv;
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
            options.AddPolicy("*",
                policy =>
                {
                    policy.WithOrigins("http://example.com",
                        "http://www.contoso.com");
                });
        });

        Env.TraversePath().Load();

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ProductMapperProfile());
            mc.AddProfile(new CategoriesMapperProfile());
        });

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddTransient<KotovskayaDbContext>();
        services.AddSingleton<KotovskayaMsContext>();

        return services;
    }
}
