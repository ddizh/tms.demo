using TMS.Api.Dto;
using TMS.Api.Extensions;
using TMS.Api.Mappers;
using TMS.Domain.Services;

namespace TMS.Api.Endpoints.Tasks;

public class GetAllTasksEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/tasks", async (ITaskService taskService) =>
            {
                var result = await taskService.GetAllAsync();
                return result.IsSuccess ? Results.Ok(result.Value.Select(x => x.ToDto())) : result.ToProblemDetails();
            })
            .WithTags("Tasks")
            .Produces<IEnumerable<TaskDto>>()
            .Produces(StatusCodes.Status400BadRequest);
    }
}