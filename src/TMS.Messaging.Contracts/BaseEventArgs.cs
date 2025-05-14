namespace TMS.Messaging.Contracts;

public abstract class BaseEventArgs
{
    public required DateTime CreatedAt { get; init; }
}