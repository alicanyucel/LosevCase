# Losev Project

Losev is a modular .NET 9 Web API application with layered architecture. It includes Application, Domain, Infrastructure, and WebAPI projects. The solution uses Entity Framework Core, MediatR, FluentValidation, AutoMapper, and JWT authentication.

## Features
- Modular architecture (Application, Domain, Infrastructure, WebAPI)
- JWT authentication
- Swagger UI for API documentation
- Entity Framework Core with SQL Server
- Automatic admin user creation on startup
- CORS support

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (or update connection string for your DB)

### Setup
1. Clone the repository:
   ```bash
   git clone <repo-url>
   ```
2. Restore packages:
   ```bash
   dotnet restore
   ```
3. Update the connection string in `appsettings.json` (in Losev.WebAPI).
4. Apply migrations:
   ```bash
   dotnet ef database update --project Losev/Losev.Infrastructure
   ```
5. Run the API:
   ```bash
   dotnet run --project Losev/Losev.WebAPI
   ```

### Usage
- Access Swagger UI at `https://localhost:<port>/swagger` for API documentation and testing.
- On first run, an admin user is created automatically:
  - Username: `admin`
  - Email: `admin@admin.com`
  - Password: `1`

## Project Structure
- **Losev.Application**: Business logic, CQRS handlers
- **Losev.Domain**: Entities, enums
- **Losev.Infrastructure**: Data access, EF Core, repositories
- **Losev.WebAPI**: API controllers, middleware, startup

## Technologies
- .NET 9
- Entity Framework Core
- MediatR
- FluentValidation
- AutoMapper
- Swashbuckle (Swagger)
- JWT Authentication

## Contributing
Pull requests are welcome. For major changes, open an issue first to discuss what you would like to change.

## License
Specify your license here.
