namespace TMS.Messaging.Contracts;

public sealed class TaskCompletedEventArgs : BaseEventArgs
{
    public int TaskId { get; init; }
    public required DateTime CompletedOn { get; init; }
}