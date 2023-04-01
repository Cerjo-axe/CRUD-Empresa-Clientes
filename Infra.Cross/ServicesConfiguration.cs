using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;

namespace Infra.Cross;

public static class ServicesConfiguration
{
    public static void AddCustomDependency(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapProfile));


        services.AddScoped<ISegmentoRepository,SegmentoRepository>();
        services.AddScoped<ISegmentoService,SegmentoService>();
        services.AddScoped<IClienteRepository,ClienteRepository>();
        services.AddScoped<IClienteService,ClienteService>();
    }
}
