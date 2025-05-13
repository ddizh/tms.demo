namespace TMS.Domain.Models;

public sealed class TaskModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public TaskModelStatus Status { get; init; }
}