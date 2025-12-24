# TodoAppUsingDotNet

This project is an ASP.NET Core Web API for managing tasks, built using Clean Architecture principles, Dependency Injection (DI), and Test-Driven Development (TDD).

The application supports creating and updating tasks while enforcing business rules such as due date validation, holiday checks, and priority limits.

---

## 🏗️ Architecture Overview

The solution follows a layered architecture:

- **TodoApp.Api**
  - Exposes REST endpoints using ASP.NET Core Web API
  - Uses Swagger for API testing

- **TodoApp.Domain**
  - Contains core entities and enums (TaskItem, Priority, TaskStatus)

- **TodoApp.Application**
  - Contains DTOs, business rules, interfaces, and services
  - No dependency on infrastructure or API

- **TodoApp.Infrastructure**
  - Implements repositories and services
  - Uses EF Core In-Memory database

- **TodoApp.Tests**
  - Contains unit tests for business rules using xUnit and FluentAssertions

---

## 📋 Business Rules Implemented

- Due date cannot be in the past
- Due date cannot fall on a weekend
- Due date cannot be on a holiday
- No more than **100 High Priority tasks** with the same due date that are not finished

---

## 🚀 How to Run the Application

### Prerequisites
- .NET 6 SDK
- Visual Studio 2022 or later

### Steps
1. Clone the repository
2. Open `TodoApp.sln` in Visual Studio
3. Set **TodoApp.Api** as the startup project
4. Press **F5** or click **Run**

Swagger UI will open automatically.

---

## 🔍 API Endpoints

- **POST** `/api/tasks` — Create a new task
- **PUT** `/api/tasks/{id}` — Update an existing task

Swagger UI can be used to test these endpoints.

---

## 🧪 Running Unit Tests

1. Open **Test Explorer** in Visual Studio
2. Click **Run All Tests**

All tests should pass successfully.

---

## 🗄️ Database

- Uses **EF Core In-Memory Database**
- No external database setup required
- Data is reset when the application stops

---

## 📌 Notes

- No frontend UI is included (Swagger is used for API testing)
- The project is structured for easy extension to SQL Server or other databases if needed

---

## 👤 Author

Yeswin Chintapalli
