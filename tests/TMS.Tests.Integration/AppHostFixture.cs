using System.Diagnostics.CodeAnalysis;

using Aspire.Hosting;

using Microsoft.Extensions.Logging;

namespace TMS.Tests.Integration;

public class AppHostFixture : IAsyncLifetime
{
    public DistributedApplication? Application { get; private set; }

    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

    public async Task InitializeAsync()
    {
        var cancellationToken = new CancellationTokenSource(DefaultTimeout).Token;
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.TMS_AppHost>(cancellationToken);

        appHost.Services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Debug);
            logging.AddFilter(appHost.Environment.ApplicationName, LogLevel.Debug);
            logging.AddFilter("Aspire.", LogLevel.Debug);
        });

        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        Application = await appHost.BuildAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);
        await Application.StartAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);
    }

    public async Task DisposeAsync()
    {
        if (Application != null)
        {
            await Application.DisposeAsync();
        }
    }
}