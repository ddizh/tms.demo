namespace TMS.Messaging.Contracts;

public class TaskCompletedEvent : ITaskEvent<TaskCompletedEventArgs>
{
    public required TaskCompletedEventArgs Args { get; init; }
}