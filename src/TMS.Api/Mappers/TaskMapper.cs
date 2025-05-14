using Riok.Mapperly.Abstractions;

using TMS.Api.Dto;
using TMS.Domain.Models;

using TaskStatus = TMS.Api.Dto.TaskStatus;

namespace TMS.Api.Mappers;

[Mapper]
public static partial class TaskMapper
{
    public static partial TaskDto ToDto(this TaskModel model);
    public static partial TaskModelStatus ToDomain(this TaskStatus status);
    private static partial TaskStatus ToDto(this TaskModelStatus status);
}