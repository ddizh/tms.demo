using System.Diagnostics;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

using TMS.Api.Extensions;
using TMS.DataAccess;
using TMS.Domain.Extensions;
using TMS.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults()
    .AddDataAccess()
    .AddMessageBus();

builder.Services.AddDomainServices()
    .AddEndpoints();

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance =
            $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
    };
});

var app = builder.Build();

app.UseApiEndpoints();
app.UseScalarApiRefence();

app.UseHttpsRedirection();
app.UseExceptionHandler();

await RunMigrationsAsync();

app.Run();

return;

async Task RunMigrationsAsync()
{
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<TmsDbContext>();
    await db.Database.MigrateAsync();
}