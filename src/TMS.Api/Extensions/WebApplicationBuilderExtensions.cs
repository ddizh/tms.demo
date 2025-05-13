using TMS.Aspire.Shared;
using TMS.DataAccess;

namespace TMS.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddDataAccess(this WebApplicationBuilder builder)
    {
        builder.AddAzureNpgsqlDbContext<TmsDbContext>(connectionName: KnownResources.DatabaseName);
    }
}