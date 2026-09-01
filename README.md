# Freaky Fashion API

> Student project created for educational purposes — coursework for the Backend2 course at EC Utbildning.

A REST API backend for a fashion e-commerce catalog, serving products grouped into categories.

## Tech Stack
- ASP.NET Core 9.0 Web API
- Entity Framework Core 9 (SQL Server provider)
- SQL Server
- OpenAPI (Microsoft.AspNetCore.OpenApi) — generates an OpenAPI JSON doc at `/openapi/v1.json` in Development

## Prerequisites
- .NET 9 SDK
- SQL Server instance (local or remote)

## Setup & Run
1. Clone the repo
2. Update the connection string in `appsettings.json` (`ConnectionStrings:Default`) if needed
3. Ensure the target database exists (note: this project has no EF Core migrations yet, so the schema must already exist / be created separately)
4. Restore and run:
   ```
   dotnet restore
   dotnet run
   ```

The API listens on the URL(s) shown in the console output.

## API Endpoints

### Products
| Method | Route | Description |
|---|---|---|
| GET | /api/products | List all products. Optional `?name=` query filters by substring match (case-sensitive). |
| GET | /api/products/{id} | Get a single product by id. 404 if not found. |
| POST | /api/products | Create a product. Body: name, description, photo, label, sku, price, kategori. Name and price (>0) required. |
| DELETE | /api/products/{id} | Delete a product. 404 if not found. |

### Categories
| Method | Route | Description |
|---|---|---|
| GET | /api/categories | List all categories, each with its nested products. |
| GET | /api/categories/{id} | Get a single category (with products) by id. 404 if not found. |
| GET | /api/categories/search?slug= | Get a category by slug. |
| POST | /api/categories | Create a category. Body: name. Slug and placeholder image are auto-generated. |
| DELETE | /api/categories/{id} | Delete a category. 404 if not found. |

## Project Structure
- `Controllers/` — API controllers (Products, Categories)
- `Domain/` — EF Core entity models
- `Dtos/` — DTOs returned by the API
- `Contracts/` — request/response contracts for write operations
- `Data/AppDbContext.cs` — EF Core DbContext and model configuration
