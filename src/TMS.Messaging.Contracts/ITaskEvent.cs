namespace TMS.Messaging.Contracts;

public interface ITaskEvent<T> : IEvent<T>, ITaskEvent;

public interface ITaskEvent
{
    public static string TargetTopicName => "tasks";
}