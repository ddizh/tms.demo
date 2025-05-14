using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using TMS.Api.Dto.Requests;
using TMS.Api.Extensions;
using TMS.Api.Mappers;
using TMS.Domain.Services;

namespace TMS.Api.Endpoints.Tasks;

public class UpdateTaskStatusEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch("/tasks/{id:int}",
                async ([FromRoute] int id,
                    [FromBody] UpdateTaskStatusRequestModel request,
                    ITaskService taskService,
                    IValidator<UpdateTaskStatusRequestModel> validator) =>
                {
                    var validationResult = await validator.ValidateAsync(request);

                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }

                    var result = await taskService.UpdateStatusAsync(id, request.NewStatus.ToDomain());

                    return result.IsSuccess ? Results.NoContent() : result.ToProblemDetails();
                })
            .WithTags("Tasks")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest);
    }
}