using Scalar.AspNetCore;

namespace TMS.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void UseScalarApiRefence(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        app.MapOpenApi();
        app.MapScalarApiReference("/api-reference", options =>
        {
            options.WithTitle("Task Management System API")
                .WithDarkMode(true)
                .WithDarkModeToggle(false);
        });

        app.MapGet("/", () => Results.Redirect("/api-reference"))
            .ExcludeFromDescription();
    }
}