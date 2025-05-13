using Microsoft.AspNetCore.Mvc;

using TMS.Domain;
using TMS.Domain.Models;

namespace TMS.Api.Endpoints.Tasks;

public class UpdateTaskStatusEndpoint: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch("/tasks/{id:int}", async ([FromRoute] int id, ITaskService taskService) =>
        {
            var result = await taskService.UpdateStatusAsync(id, TaskModelStatus.Completed);
            
            return Results.NoContent();
        });
    }
}