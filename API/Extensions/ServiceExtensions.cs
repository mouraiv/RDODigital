// Extensions/ServiceExtensions.cs
using Infra.Data;
using Domain.Interfaces;
using Infra.Repositories;
using Application.Interfaces;
using Application.Services;
using Domain.Repositories;

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
        services.AddScoped<IStatusConexaoRepository, StatusConexaoRepository>();
        services.AddScoped<ICargoRepository ,CargoRepository>();
        services.AddScoped<IClienteRepository ,ClienteRepository>();
        services.AddScoped<IAtividadeRepository ,AtividadeRepository>();
        services.AddScoped<IProjetoRepository ,ProjetoRepository>();
        services.AddScoped<IMaterialRepository ,MaterialRepository>();
        
        // Services
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IFileUserService, FileUserService>();
        services.AddScoped<IStatusConexaoService, StatusConexaoService>();
        services.AddScoped<ICargoService, CargoService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IAtividadeService, AtividadeService>();
        services.AddScoped<IProjetoService, ProjetoService>();
        services.AddScoped<IMaterialService, MaterialService>();
        
        return services;
    }
}