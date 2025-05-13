using Microsoft.Extensions.DependencyInjection;

namespace TMS.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient<ITaskService, TaskService>();

        return services;
    }
}