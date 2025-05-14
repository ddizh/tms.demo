namespace TMS.Api.Dto;

public sealed class TaskDto
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public TaskStatus Status { get; init; }
}