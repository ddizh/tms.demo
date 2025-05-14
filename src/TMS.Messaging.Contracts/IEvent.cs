namespace TMS.Messaging.Contracts;

public interface IEvent<T>
{
    T Args { get; init; }
}