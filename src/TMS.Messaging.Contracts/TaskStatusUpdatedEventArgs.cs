namespace TMS.Messaging.Contracts;

public sealed class TaskStatusUpdatedEventArgs : BaseEventArgs
{
    public int TaskId { get; init; }
    public int OldStatus { get; init; }
    public int NewStatus { get; init; }
}