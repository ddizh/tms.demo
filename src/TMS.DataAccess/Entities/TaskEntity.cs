namespace TMS.DataAccess.Entities;

public sealed class TaskEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public TaskEntityStatus Status { get; set; }
}