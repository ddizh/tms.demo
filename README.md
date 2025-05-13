## TMS (Task Management System)

A simple task management application which consists of:

- .NET Aspire host
- ASP .NET Core API
- Azure Service Bus pub/sub via local emulator
- Entity Framework Core
- Scalar as OpenApi UI reference

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Docker desktop

## How to run

```dotnet run --project src/TMS.AppHost -c Release```

## Things to consider / improve

- Separate project for ef migrations should
  help separate concerns and slightly reduce docker image size. 
- [Central package management](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management) to avoid dependency hell in the future.
- 