using TMS.Domain;

namespace TMS.Api.Endpoints.Tasks;

public class CreateTaskEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/tasks",
                async (ITaskService taskService) =>
                    await taskService.CreateAsync("test task", Guid.NewGuid().ToString()))
            .WithDisplayName("Create new task");
    }
}