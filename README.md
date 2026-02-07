📝 Todo API – ASP.NET Core Web API

A clean and well-structured RESTful Todo API built with ASP.NET Core Web API and Entity Framework Core.
This project demonstrates backend best practices, clean architecture, DTO usage, validation, and proper error handling.

The API supports full CRUD operations, filtering, pagination, and includes a dedicated endpoint to mark todos as completed.

🚀 Tech Stack

- ASP.NET Core Web API (.NET 8)

- Entity Framework Core

- SQL Server

- Swagger / OpenAPI

- LINQ

RESTful API design

📁 Project Structure
TodoApi/
├── Controllers
├── Data
│ ├── TodoDbContext.cs
│ └── DbInitializer.cs
├── DTOs
│ ├── TodoCreateDto.cs
│ ├── TodoUpdateDto.cs
│ └── TodoReadDto.cs
├── Entities
│ └── TodoItem.cs
├── Middleware
│ ├── ExceptionMiddleware.cs
│ └── ExceptionMiddlewareExtensions.cs
├── Services
│ ├── ITodoService.cs
│ └── TodoService.cs
├── Migrations
├── Program.cs
├── appsettings.json
├── TodoApi.http
└── TodoApi.csproj

The project follows separation of concerns:

Controllers → HTTP layer

Services → business logic

Data → database access

DTOs → API contracts

🧩 Entity Overview
TodoItem
Field Description
Id Unique identifier
Title Required, min 3 – max 100 characters
Description Optional, max 500 characters
IsCompleted Completion status
DueDate Optional due date
Priority Integer value between 1–3
CreatedAt Automatically set on creation
UpdatedAt Automatically updated on changes
🔗 API Endpoints
CRUD Operations
Method Endpoint Description
GET /api/todos Get all todos (supports filtering & pagination)
GET /api/todos/{id} Get todo by id
POST /api/todos Create a new todo
PUT /api/todos/{id} Update an existing todo
DELETE /api/todos/{id} Delete a todo
Extra Endpoint
Method Endpoint Description
PATCH /api/todos/{id}/complete Marks a todo as completed
🔍 Filtering & Pagination

GET /api/todos supports optional query parameters:

isCompleted → true / false

search → searches in title

page → page number (default: 1)

pageSize → items per page (default: 10, max: 100)

Example:

GET /api/todos?isCompleted=false&search=api&page=1&pageSize=5

✅ Validation & Error Handling

DTO-based validation using Data Annotations

Automatic 400 Bad Request on invalid input

404 Not Found for missing resources

Global exception handling middleware:

Logs unexpected errors

Returns generic 500 Internal Server Error

🗄️ Database & Migrations

SQL Server database

Database is created via EF Core migrations

Seed data is added automatically if the database is empty

Migration Commands
Add-Migration InitialCreate
Update-Database

⚙️ Configuration
appsettings.json

Contains safe placeholder configuration.

appsettings.Development.json (not committed)

Create this file locally for your connection string:

{
"ConnectionStrings": {
"DefaultConnection": "<your-sql-server-connection-string>"
}
}

📄 Swagger & API Documentation

Swagger UI enabled

XML comments added for endpoints

Clear request/response documentation

Access Swagger at:

https://localhost:<port>/swagger

🧪 Testing

A TodoApi.http file is included to test endpoints directly from Visual Studio without Postman.

⭐ Highlights

Clean architecture & service abstraction

DTO-based API design

Global exception handling

Seed data for demo purposes

Swagger documentation

Git-safe configuration handling

📌 Purpose

This project was built as a portfolio-ready backend assignment to demonstrate:

ASP.NET Core Web API fundamentals

Clean code & architecture principles

Real-world backend development practices
