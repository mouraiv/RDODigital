// Extensions/ServiceExtensions.cs
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infra.Data;
using Domain.Interfaces;
using Infra.Repositories;
using Application.Interfaces;
using Application.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Registre o DapperContext como Scoped
        services.AddScoped<DapperContext>(provider => 
            new DapperContext(configuration));
        
        return services;
    }

    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
    {
        // Repositórios
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        
        // Services
        services.AddScoped<IUsuarioService, UsuarioService>();
        
        return services;
    }
}