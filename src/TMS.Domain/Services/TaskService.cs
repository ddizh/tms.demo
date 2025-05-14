using FluentResults;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TMS.Messaging.Contracts;
using TMS.DataAccess;
using TMS.DataAccess.Entities;
using TMS.Domain.Mappers;
using TMS.Domain.Models;

using Wolverine;

namespace TMS.Domain.Services;

public class TaskService(TmsDbContext dbContext, IMessageBus messageBus, ILogger<TaskService> logger) : ITaskService
{
    public async Task<Result<TaskModel>> CreateAsync(string name, string? description)
    {
        try
        {
            var entity = new TaskEntity
            {
                Name = name,
                Description = description,
                Status = TaskEntityStatus.NotStarted
            };
            
            await dbContext.Tasks.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            
            return entity.ToDomain();
        }
        catch (Exception e)
        {
            logger.LogError("Unable to create task with name {Name} : {ErrorMessage}.", name, e.Message);
            return Result.Fail<TaskModel>($"Unable to create task with name {name}.");
        }
    }

    public async Task<Result> UpdateStatusAsync(int id, TaskModelStatus status)
    {
        var entity = dbContext.Tasks.FirstOrDefault(x => x.Id == id);

        if (entity == null)
        {
            return Result.Fail($"Task with id {id} not found.");
        }

        var oldStatus = entity.Status;
        var newStatus = status.ToEntity();

        switch (newStatus)
        {
            case TaskEntityStatus.NotStarted:
                {
                    return Result.Fail($"Task with id {id} has already been started.");
                }
            case TaskEntityStatus.InProgress when oldStatus == TaskEntityStatus.NotStarted:
            case TaskEntityStatus.Completed when oldStatus == TaskEntityStatus.InProgress:
                {
                    entity.Status = newStatus;
                    break;
                }
            default:
                {
                    return Result.Fail($"Task {id} can't be updated.");
                }
        }

        var updated = await dbContext.SaveChangesAsync();

        if (updated == 0)
        {
            return Result.Fail($"Task with id {id} has not been updated.");
        }

        await messageBus.PublishAsync(new TaskStatusUpdatedEvent
        {
            Args = new TaskStatusUpdatedEventArgs
            {
                TaskId = entity.Id, 
                OldStatus = (int)oldStatus, 
                NewStatus = (int)newStatus,
                CreatedAt = DateTime.UtcNow
            }
        });

        return Result.Ok();
    }

    public async Task<Result<IReadOnlyCollection<TaskModel>>> GetAllAsync()
    {
        try
        {
            var tasks = await dbContext.Tasks.Select(x => x.ToDomain())
                .ToListAsync();

            return tasks;
        }
        catch (Exception e)
        {
            logger.LogError("Unable to fetch all tasks: {ErrorMessage}", e.Message);
            return Result.Fail<IReadOnlyCollection<TaskModel>>($"Unable to fetch all tasks.");
        }
    }
}