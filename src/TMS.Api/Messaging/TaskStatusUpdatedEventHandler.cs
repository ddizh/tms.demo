using TMS.Messaging.Contracts;

namespace TMS.Api.Messaging;

public sealed class TaskStatusUpdatedEventHandler
{
    public void Handle(TaskStatusUpdatedEvent @event, ILogger<TaskStatusUpdatedEventHandler> logger)
    {
        logger.LogInformation(
            "Handling {EventName}. Task id: {TaskId}, old status: {OldStatus}, new status: {NewStatus}",
            nameof(TaskStatusUpdatedEvent), @event.Args.TaskId, @event.Args.OldStatus, @event.Args.NewStatus);
    }
}