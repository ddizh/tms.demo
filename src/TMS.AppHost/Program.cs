using TMS.Aspire.Shared;

var builder = DistributedApplication.CreateBuilder(args);

var db = ConfigureDatabaseResources();

_ = builder.AddProject<Projects.TMS_Api>(KnownResources.ApiResourceName)
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();

return;


IResourceBuilder<IResourceWithConnectionString> ConfigureDatabaseResources()
{
    var postgres = builder.AddPostgres(KnownResources.PostgresContainerResourceName)
        .WithLifetime(ContainerLifetime.Persistent)
        .WithPgAdmin();

    return postgres.AddDatabase(KnownResources.DatabaseName);
}