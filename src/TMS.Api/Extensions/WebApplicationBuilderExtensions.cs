using TMS.DataAccess;
using TMS.Messaging.Contracts;
using TMS.ServiceDefaults;

using Wolverine;
using Wolverine.RabbitMQ;

namespace TMS.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddMessageBus(this WebApplicationBuilder builder)
    {
        builder.Services.AddWolverine(options =>
        {
            var taskQueueName = $"events-{ITaskEvent.TargetTopicName}";
            options.ListenToRabbitQueue(taskQueueName)
                .TelemetryEnabled(true);

            options.Publish(rule =>
            {
                rule.MessagesImplementing<ITaskEvent>();
                rule.ToRabbitExchange($"exchange-{taskQueueName}", exchange =>
                    {
                        exchange.BindQueue(taskQueueName);
                        exchange.ExchangeType = ExchangeType.Fanout;
                    })
                    .TelemetryEnabled(true);
            });

            options.UseRabbitMqUsingNamedConnection(KnownResources.MessageBusResourceName)
                .AutoProvision();
        });

        builder.Services.AddOpenTelemetry()
            .WithTracing(x => x.AddSource("Wolverine"));
    }

    public static WebApplicationBuilder AddDataAccess(this WebApplicationBuilder builder)
    {
        builder.AddAzureNpgsqlDbContext<TmsDbContext>(connectionName: KnownResources.DatabaseName);

        return builder;
    }
}