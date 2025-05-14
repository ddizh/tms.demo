using TMS.Messaging.Contracts;

namespace TMS.Api.Messaging;

public sealed class TaskCompletedEventHandler
{
    public void Handle(TaskCompletedEvent @event, ILogger<TaskCompletedEventHandler> logger)
    {
        logger.LogInformation(
            "Handling {EventName}. Task with id: {TaskId} has been completed on {CompletedOn}",
            nameof(TaskCompletedEvent), @event.Args.TaskId, @event.Args.CompletedOn);
    }
}