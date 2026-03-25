# Blog API

Simple ASP.NET Core Web API for blog user management, authentication, and role assignment.

## Contents

- Architecture (Clean Architecture)
- Tech stack
- Project structure
- Quick start
- Configuration
- Database migrations
- API endpoints + examples

## Architecture

This project follows **Clean Architecture**.

Dependency rule:

- Outer layers can depend on inner layers.
- Inner layers must not depend on outer layers.

In this solution:

- `Domain` is the core (entities and contracts).
- `Application` contains use cases/business logic and depends only on `Domain`.
- `Infrastructure` contains EF Core and repository implementations and depends on `Application` + `Domain`.
- `API` is the presentation layer (controllers, DI, startup) and depends on `Application` + `Infrastructure`.

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (SQL Server)
- JWT Authentication
- FluentValidation
- Swagger (OpenAPI)

## Project Structure

- `src/API`: Presentation layer (controllers, middleware, startup)
- `src/Application`: Application layer (use cases, DTOs, validators, services)
- `src/Domain`: Domain layer (entities, repository/unit-of-work contracts)
- `src/Infrastructure`: Infrastructure layer (EF Core, repositories, migrations)
- `tests`: unit and functional test projects

## Request Flow

Typical flow for an API request:

1. `API` controller receives request.
2. Controller calls `Application` service.
3. `Application` service uses repository interfaces from `Domain`.
4. `Infrastructure` provides concrete repository implementations.
5. Data is returned back through `Application` to `API` response.

## Prerequisites

Before running the project, make sure you have:

- .NET 8 SDK installed
- SQL Server running locally (or update connection string to your server)

## Configuration

Main configuration file:

- `src/API/appsettings.json`

Important settings:

- `ConnectionStrings:DefaultConnection`
- `Jwt:Key`
- `Jwt:Issuer`
- `Jwt:Audience`

Default local SQL Server connection is already provided in `appsettings.json`.

## Quick Start

1. Ensure SQL Server is running and the connection string is correct.
2. Apply EF Core migrations.
3. Run the API and open Swagger.

### Option A: Run with your local SQL Server

- Update `src/API/appsettings.json` → `ConnectionStrings:DefaultConnection` if needed.

### Option B: Run SQL Server using Docker (optional)

If you prefer Docker, you can run SQL Server like this:

```bash
docker run --name blog-sql -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong!Passw0rd" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

Then update `src/API/appsettings.json` to match the password you chose.

## How To Run

From the solution root:

```bash
dotnet restore
dotnet build
dotnet run --project src/API/API.csproj
```

API will run on:

- `http://localhost:5017`
- `https://localhost:7142`

Swagger UI:

- `https://localhost:7142/swagger`

## Database Migration

Migrations already exist in `src/Infrastructure/Migrations`.

If you need to apply migrations:

```bash
dotnet ef database update --project src/Infrastructure --startup-project src/API
```

If `dotnet ef` is not installed:

```bash
dotnet tool install --global dotnet-ef
```

## Main API Endpoints

Base route: `api/[controller]`

Authentication:

- `POST /api/auth/register`
- `POST /api/auth/login`

Users:

- `GET /api/user?pageNumber=1&pageSize=10`
- `GET /api/user/{id}`
- `PUT /api/user`
- `DELETE /api/user/{id}`
- `POST /api/user/assign-role`

## Example Requests (curl)

Base URL (HTTP): `http://localhost:5017`

> On Windows PowerShell you can use either `Invoke-RestMethod` (recommended) or `curl.exe`.

### Register (PowerShell)

```powershell
$body = @{ email = "test@example.com"; password = "P@ssw0rd!"; userName = "testuser" } | ConvertTo-Json
Invoke-RestMethod -Method Post -Uri "http://localhost:5017/api/auth/register" -ContentType "application/json" -Body $body
```

### Login (PowerShell)

```powershell
$body = @{ email = "test@example.com"; password = "P@ssw0rd!" } | ConvertTo-Json
Invoke-RestMethod -Method Post -Uri "http://localhost:5017/api/auth/login" -ContentType "application/json" -Body $body
```

### Register

```bash
curl -X POST "http://localhost:5017/api/auth/register" -H "Content-Type: application/json" -d "{\"email\":\"test@example.com\",\"password\":\"P@ssw0rd!\",\"userName\":\"testuser\"}"
```

### Login

```bash
curl -X POST "http://localhost:5017/api/auth/login" -H "Content-Type: application/json" -d "{\"email\":\"test@example.com\",\"password\":\"P@ssw0rd!\"}"
```

### Get users (paged)

```bash
curl "http://localhost:5017/api/user?pageNumber=1&pageSize=10"
```

### Get user by id

```bash
curl "http://localhost:5017/api/user/1"
```

### Update user

```bash
curl -X PUT "http://localhost:5017/api/user" -H "Content-Type: application/json" -d "{\"id\":1,\"userName\":\"newname\",\"email\":\"new@example.com\"}"
```

### Delete user

```bash
curl -X DELETE "http://localhost:5017/api/user/1"
```

### Assign role to user

```bash
curl -X POST "http://localhost:5017/api/user/assign-role" -H "Content-Type: application/json" -d "{\"userId\":1,\"roleId\":1}"
```

## Development Notes

- Swagger is enabled in Development environment.
- Keep secrets (DB password, JWT key) out of source control for production.

## Learning Reference

This project appears to be based on this learning playlist:

- [YouTube playlist](https://youtube.com/playlist?list=PLc2Ziv7051baIPDSKJcnob8chhURMgdL6&si=l0y5myuirxHwTNcR)
