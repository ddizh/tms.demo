using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using TMS.Api.Dto;
using TMS.Api.Dto.Requests;
using TMS.Api.Extensions;
using TMS.Api.Mappers;
using TMS.Domain.Services;

namespace TMS.Api.Endpoints.Tasks;

public class CreateTaskEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/tasks",
                async ([FromBody] CreateTaskRequestModel request,
                    ITaskService taskService,
                    IValidator<CreateTaskRequestModel> validator) =>
                {
                    var validationResult = await validator.ValidateAsync(request);

                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }

                    var result = await taskService.CreateAsync(request.Name, request.Description);
                    
                    return result.IsSuccess ? Results.Ok(result.Value.ToDto()) : result.ToProblemDetails();
                })
            .WithTags("Tasks")
            .Produces<TaskDto>()
            .Produces(StatusCodes.Status400BadRequest);
    }
}