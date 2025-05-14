using FluentResults;

using Microsoft.AspNetCore.Mvc;

namespace TMS.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails<T>(this Result<T> result)
    {
        return GetProblemDetails(result.Errors);
    }

    public static IResult ToProblemDetails(this Result result)
    {
        return GetProblemDetails(result.Errors);
    }

    private static IResult GetProblemDetails(this List<IError> errors)
    {
        var pd = new ProblemDetails
        {
            Title = "One or more errors occurred.",
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1", // bad request
            Extensions = new Dictionary<string, object?> { { "errors", errors.Select(e => e.Message) } }
        };

        return Results.Problem(pd);
    }
}