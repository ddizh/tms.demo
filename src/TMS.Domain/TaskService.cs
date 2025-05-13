using Microsoft.EntityFrameworkCore;

using TMS.DataAccess;
using TMS.DataAccess.Entities.Task;
using TMS.Domain.Mappers;
using TMS.Domain.Models;

namespace TMS.Domain;

public class TaskService(TmsDbContext dbContext) : ITaskService
{
    public async Task<TaskModel> CreateAsync(string name, string? description)
    {
        var entity = new TaskEntity { Name = name, Description = description, Status = TaskEntityStatus.NotStarted };
        await dbContext.Tasks.AddAsync(entity);

        await dbContext.SaveChangesAsync();

        return entity.ToDomain();
    }

    public async Task<bool> UpdateStatusAsync(int id, TaskModelStatus status)
    {
        var entity = dbContext.Tasks.FirstOrDefault(x => x.Id == id);

        if (entity == null)
        {
            return false;
        }

        var entityStatus = status.ToEntity();

        switch (entityStatus)
        {
            case TaskEntityStatus.NotStarted:
                {
                    return false;
                }
            case TaskEntityStatus.InProgress when entity.Status == TaskEntityStatus.NotStarted:
            case TaskEntityStatus.Completed when entity.Status == TaskEntityStatus.InProgress:
                {
                    entity.Status = entityStatus;
                    break;
                }
            default:
                {
                    return false;
                }
        }

        var updated = await dbContext.SaveChangesAsync();

        return updated > 0;
    }

    public async Task<IReadOnlyCollection<TaskModel>> GetAllAsync()
    {
        return await dbContext.Tasks.Select(x => x.ToDomain())
            .ToListAsync();
    }
}

public interface ITaskService
{
    Task<TaskModel> CreateAsync(string name, string? description);
    Task<bool> UpdateStatusAsync(int id, TaskModelStatus status);
    Task<IReadOnlyCollection<TaskModel>> GetAllAsync();
}