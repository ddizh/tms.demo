using System.Text.Json.Serialization;

namespace TMS.Api.Dto;

[JsonConverter(typeof(JsonStringEnumConverter<TaskStatus>))]
public enum TaskStatus
{
    NotStarted = 0,
    InProgress = 1,
    Completed = 2
}