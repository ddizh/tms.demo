namespace TMS.Api.Dto.Requests;

public sealed class CreateTaskRequestModel
{
    public required string Name { get; init; }
    public string? Description { get; init; }
}