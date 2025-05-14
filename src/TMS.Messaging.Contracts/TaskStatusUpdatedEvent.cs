namespace TMS.Messaging.Contracts;

public class TaskStatusUpdatedEvent : ITaskEvent<TaskStatusUpdatedEventArgs>
{
    public required TaskStatusUpdatedEventArgs Args { get; init; }
}