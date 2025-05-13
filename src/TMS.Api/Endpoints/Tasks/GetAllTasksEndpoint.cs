using TMS.Domain;

namespace TMS.Api.Endpoints.Tasks;

public class GetAllTasksEndpoint: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/tasks", async (ITaskService taskService) => await taskService.GetAllAsync());
    }
}