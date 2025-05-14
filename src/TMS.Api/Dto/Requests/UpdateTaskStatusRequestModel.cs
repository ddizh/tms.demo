namespace TMS.Api.Dto.Requests;

public sealed class UpdateTaskStatusRequestModel
{
    public TaskStatus NewStatus { get; init; }
}