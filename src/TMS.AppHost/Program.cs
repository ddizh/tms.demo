using TMS.ServiceDefaults;

var builder = DistributedApplication.CreateBuilder(args);

var db = ConfigureDatabaseResources();
var messageBus = ConfigureMessageBusResources();

_ = builder.AddProject<Projects.TMS_Api>(KnownResources.ApiResourceName)
    .WithReference(db)
    .WithReference(messageBus)
    .WaitFor(db)
    .WaitFor(messageBus);

builder.Build().Run();

return;

IResourceBuilder<IResourceWithConnectionString> ConfigureMessageBusResources()
{
    var username =
        builder.AddParameterFromConfiguration("rabbitmq-username", "Parameters:RabbitMq:Management:UserName",
            secret: true);
    var password =
        builder.AddParameterFromConfiguration("rabbitmq-password", "Parameters:RabbitMq:Management:Password",
            secret: true);

    var rabbitMq = builder.AddRabbitMQ(KnownResources.MessageBusResourceName, username, password)
        .WithManagementPlugin();

    return rabbitMq;
}

IResourceBuilder<IResourceWithConnectionString> ConfigureDatabaseResources()
{
    var postgres = builder.AddPostgres(KnownResources.PostgresContainerResourceName)
        .WithLifetime(ContainerLifetime.Persistent)
        .WithPgAdmin();

    return postgres.AddDatabase(KnownResources.DatabaseName);
}