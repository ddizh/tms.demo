using Microsoft.EntityFrameworkCore;

using TMS.Api.Extensions;
using TMS.DataAccess;
using TMS.Domain.Extensions;
using TMS.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults()
    .AddDataAccess();

builder.Services.AddDomainServices()
    .AddEndpoints();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseApiEndpoints();
app.UseScalarApiRefence();

app.UseHttpsRedirection();

await RunMigrationsAsync();

app.Run();

return;

async Task RunMigrationsAsync()
{
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<TmsDbContext>();
    await db.Database.MigrateAsync();
}