using Microsoft.Extensions.DependencyInjection;

using TMS.Domain.Services;

namespace TMS.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient<ITaskService, TaskService>();

        return services;
    }
}