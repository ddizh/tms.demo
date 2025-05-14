## TMS (Task Management System)

A simple task management application which consists of:

- .NET Aspire host
- ASP .NET Core API
- Simple RabbitMQ pub/sub via fanout exchange
- [Wolverine](https://github.com/JasperFx/wolverine) for message bus abstraction
- [Mapperly](https://github.com/riok/mapperly) for source-generated model mappings
- Entity Framework Core with PostgreSQL
- [Scalar](https://github.com/scalar/scalar) as OpenApi UI reference

### Solution structure

.NET Aspire host and related infrastructure projects:
- **TMS.AppHost**
- **TMS.ServiceDefaults**

**TMS.Api** – CRUD endpoints, OpenAPI reference  
**TMS.Domain** – domain models and business logic  
**TMS.DataAccess** – ef db context, migrations and entity models   
**TMS.Messaging.Contracts** – shared event contracts

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Docker desktop

## How to run

```dotnet run --project src/TMS.AppHost -c Release```

The default username/password for RabbitMQ dashboard is `admin/admin`.

## Things to consider / improve

- Separate project for ef migrations should
  help separate concerns and slightly reduce the docker image size.
- [Central package management](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management) to
  avoid dependency hell in the future.
- Use [transactional outbox pattern](https://wolverinefx.net/guide/durability/) for status update event to ensure data
  consistency.
- If tasks can only transition from one state into another, then there is no need for us to take any user inputs at all.
  We could just expose the 'ProgressTaskStatus' endpoint and update it's status internally without relying on user
  input.
- Move event consumer into its own service/process.
- Pagination for GetAllTasks endpoint.
- Optimistic concurrency handling.