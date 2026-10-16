# Task 4 — Task Management API (EF Core + SQLite)

An ASP.NET Core REST API that stores tasks in SQLite using Entity Framework Core migrations. The controller calls a service layer, and the service uses `AppDbContext` for data access.

## Requirements
- .NET 8 SDK
- Internet access on first restore for NuGet packages
- EF Core CLI (`dotnet-ef`) for migrations

## Setup and run
From the directory containing `Task4TaskApi.csproj`:

```bash
dotnet restore
dotnet build
dotnet tool install --global dotnet-ef --version 8.0.11
```

If `dotnet-ef` is already installed, use `dotnet tool update --global dotnet-ef --version 8.0.11` instead.

Create the initial migration (run once, before submitting the project):

```bash
dotnet ef migrations add InitialCreate
```

Apply migrations to create/update the SQLite database:

```bash
dotnet ef database update
```

Start the API:

```bash
dotnet run
```

Open the Swagger URL printed in the terminal when the app starts.

## Endpoints
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Tasks?page=1&pageSize=10` | List tasks with paging |
| GET | `/api/Tasks?isCompleted=false&page=1&pageSize=10` | Filter by completion and page results |
| GET | `/api/Tasks/{id}` | Get one task |
| POST | `/api/Tasks` | Create a task |
| PUT | `/api/Tasks/{id}` | Update a task |
| DELETE | `/api/Tasks/{id}` | Delete a task |

Page starts at 1; `pageSize` must be 1–100. The list response includes `items`, `page`, `pageSize`, `totalCount`, and `totalPages`.

## Example request bodies
Create:
```json
{
  "title": "Finish internship Task 4",
  "description": "Add migrations, test the API, and publish to GitHub"
}
```

Update:
```json
{
  "title": "Finish internship Task 4",
  "description": "Test endpoints and publish the repository",
  "isCompleted": true
}
```

## Architecture
- **Controller:** HTTP routes and status codes.
- **Service layer:** task operations and filtering/paging logic.
- **EF Core DbContext:** maps task model to SQLite.
- **Migrations:** version and apply database schema changes; don't create the database manually.

SQLite writes to `tasks.db` in the application's working directory. The `.gitignore` excludes database files, while migration files and source code recreate the schema. For later model changes, run `dotnet ef migrations add <MigrationName>` followed by `dotnet ef database update`.
