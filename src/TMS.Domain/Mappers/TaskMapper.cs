using Riok.Mapperly.Abstractions;

using TMS.DataAccess.Entities;
using TMS.Domain.Models;

namespace TMS.Domain.Mappers;

[Mapper]
public static partial class TaskMapper
{
    public static partial TaskModel ToDomain(this TaskEntity entity);
    public static partial TaskEntityStatus ToEntity(this TaskModelStatus status);
    private static partial TaskModelStatus ToDomain(this TaskEntityStatus status);
}