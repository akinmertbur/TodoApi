# 📝 Todo API – ASP.NET Core Web API

A clean, well-structured **RESTful Todo API** built with **ASP.NET Core Web API** and **Entity Framework Core**.  
This project demonstrates **backend best practices**, **clean architecture**, **DTO-based design**, **validation**, and **robust error handling**.

The API supports full **CRUD operations**, **filtering**, **pagination**, and includes a dedicated endpoint to **mark todos as completed**.

---

## 🚀 Tech Stack

- **ASP.NET Core Web API (.NET 8)**
- **Entity Framework Core**
- **SQL Server**
- **Swagger / OpenAPI**
- **LINQ**
- **RESTful API Design**

---

## 📁 Project Structure

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


---

## 🧱 Architecture Overview

The project follows **Separation of Concerns**:

- **Controllers** → HTTP layer & request handling  
- **Services** → Business logic  
- **Data** → Database access & EF Core configuration  
- **DTOs** → API contracts (request/response models)  

This structure ensures **maintainability**, **testability**, and **scalability**.

---

## 🧩 Entity Overview

### TodoItem

| Field        | Description |
|--------------|------------|
| `Id`         | Unique identifier |
| `Title`      | Required, min 3 – max 100 characters |
| `Description`| Optional, max 500 characters |
| `IsCompleted`| Completion status |
| `DueDate`    | Optional due date |
| `Priority`   | Integer value between **1–3** |
| `CreatedAt`  | Automatically set on creation |
| `UpdatedAt`  | Automatically updated on changes |

---

## 🔗 API Endpoints

### CRUD Operations

| Method | Endpoint | Description |
|------|---------|------------|
| `GET` | `/api/todos` | Get all todos (supports filtering & pagination) |
| `GET` | `/api/todos/{id}` | Get todo by id |
| `POST` | `/api/todos` | Create a new todo |
| `PUT` | `/api/todos/{id}` | Update an existing todo |
| `DELETE` | `/api/todos/{id}` | Delete a todo |

### Extra Endpoint

| Method | Endpoint | Description |
|------|---------|------------|
| `PATCH` | `/api/todos/{id}/complete` | Mark a todo as completed |

---

## 🔍 Filtering & Pagination

`GET /api/todos` supports optional query parameters:

| Parameter | Description |
|---------|------------|
| `isCompleted` | `true` / `false` |
| `search` | Searches in **title** |
| `page` | Page number (default: `1`) |
| `pageSize` | Items per page (default: `10`, max: `100`) |

### Example
GET /api/todos?isCompleted=false&search=api&page=1&pageSize=5


---

## ✅ Validation & Error Handling

### Validation
- DTO-based validation using **Data Annotations**
- Automatic **400 Bad Request** on invalid input
- **404 Not Found** for missing resources

### Global Exception Handling
- Custom middleware catches unhandled exceptions
- Logs unexpected errors
- Returns a generic **500 Internal Server Error** response

---

## 🗄️ Database & Migrations

- **SQL Server** database
- Database is created using **EF Core migrations**
- Seed data is added automatically if the database is empty

### Migration Commands

```powershell
Add-Migration InitialCreate
Update-Database
```

## ⚙️ Configuration

appsettings.json

Contains safe placeholder configuration.

Create appsettings.Development.json locally (not committed):

```powershell
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  }
}
```

## 📄 Swagger & API Documentation

Swagger UI enabled

XML comments added for endpoints

Clear request & response documentation

Access Swagger at:

```powershell
https://localhost:{PORT}/swagger
```

## 🧪 Testing

A TodoApi.http file is included to test endpoints directly from Visual Studio
(no Postman required).

## ⭐ Highlights

- Clean architecture & service abstraction

- DTO-based API design

- Global exception handling middleware

- Seed data for demo purposes

- Swagger documentation

- Git-safe configuration handling
