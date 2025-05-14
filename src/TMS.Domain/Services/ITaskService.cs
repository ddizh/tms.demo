using FluentResults;

using TMS.Domain.Models;

namespace TMS.Domain.Services;

public interface ITaskService
{
    Task<Result<TaskModel>> CreateAsync(string name, string? description);
    Task<Result> UpdateStatusAsync(int id, TaskModelStatus status);
    Task<Result<IReadOnlyCollection<TaskModel>>> GetAllAsync();
}