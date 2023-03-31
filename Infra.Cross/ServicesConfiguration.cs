using Domain.Interfaces.Repository;
using Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Cross;

public static class ServicesConfiguration
{
    public static void AddCustomDependency(this IServiceCollection services)
    {
        services.AddScoped<ISegmentoRepository,SegmentoRepository>();
    }
}
