# ADO.NET API 1

An ASP.NET Core Web API project demonstrating direct SQL Server data access with ADO.NET. The application is organized into controllers, DTOs, data-access components, models, and a database-related project area.

## Technology Stack
- .NET 9 / ASP.NET Core Web API
- C#
- ADO.NET
- Microsoft.Data.SqlClient
- SQL Server
- Swagger / Swashbuckle

## Project Structure
- `Controllers/` — HTTP endpoints, including the student controller.
- `DTOs/` — request and response data-transfer objects.
- `Data/` — data-access implementation.
- `Database/` — database-related assets.
- `Models/` — application models.
- `Program.cs` — application startup and service configuration.

## Getting Started
1. Install the .NET 9 SDK.
2. Ensure SQL Server is available and create/configure the database expected by the project.
3. Set the connection string in user secrets or local configuration. Do not commit credentials.
4. Restore and run:
   ```bash
   dotnet restore
   dotnet run --project ADO.NET_API_1
   ```
5. Open the Swagger UI URL printed by the application to explore the API.

Inspect the student controller and data-access classes for the implemented operations and SQL commands. This project is intended to illustrate explicit database connectivity and command execution using ADO.NET rather than an ORM.