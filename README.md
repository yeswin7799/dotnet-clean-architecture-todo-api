# Todo App – Clean Architecture ASP.NET Core API

[![CI](https://github.com/yeswin7799/dotnet-clean-architecture-todo-api/actions/workflows/dotnet-ci.yml/badge.svg)](https://github.com/yeswin7799/dotnet-clean-architecture-todo-api/actions/workflows/dotnet-ci.yml)
![.NET](https://img.shields.io/badge/.NET-6.0-blue)

A production-style **ASP.NET Core Web API** for task management, built using **Clean Architecture**, **SOLID principles**, and **Test-Driven Development (TDD)**.  
This project focuses on enforcing real-world business rules while keeping the system modular, testable, and maintainable.

---

## 🚀 Tech Stack

- **Backend:** ASP.NET Core 6, C#
- **Architecture:** Clean Architecture, Dependency Injection
- **Data Access:** Entity Framework Core (In-Memory)
- **Testing:** xUnit, FluentAssertions
- **API Documentation:** Swagger / OpenAPI
- **CI/CD:** GitHub Actions

---

## 🏗️ Architecture Overview

The solution follows **Clean Architecture**, ensuring business logic is independent of frameworks and infrastructure.

TodoApp.Api
↓
TodoApp.Application
↓
TodoApp.Domain
↑
TodoApp.Infrastructure

### Project Structure

- **TodoApp.Api**
  - ASP.NET Core Web API
  - Exposes REST endpoints
  - Swagger-enabled for API testing

- **TodoApp.Domain**
  - Core business entities and enums
  - `TaskItem`, `Priority`, `TaskStatus`
  - No dependencies on other layers

- **TodoApp.Application**
  - Business rules and use cases
  - DTOs, interfaces, services
  - Framework-agnostic

- **TodoApp.Infrastructure**
  - Repository implementations
  - EF Core In-Memory database

- **TodoApp.Tests**
  - Unit tests for business rules
  - Written using xUnit and FluentAssertions

---

## 📋 Business Rules Implemented

The API enforces the following rules:

- ❌ Due date cannot be in the past
- ❌ Due date cannot fall on a weekend
- ❌ Due date cannot be on a holiday
- ❌ Maximum **100 High-Priority tasks** with the same due date that are not completed

All rules are validated through **unit tests**.

---

## 🔍 API Endpoints

| Method | Endpoint | Description |
|------|--------|------------|
| POST | `/api/tasks` | Create a new task |
| PUT | `/api/tasks/{id}` | Update an existing task |

Swagger UI is available for interactive testing.

---

## 🧪 Testing

- Business rules are covered using **unit tests**
- Tests are executed automatically via **GitHub Actions**
- Ensures regression prevention and code stability

To run tests locally:
1. Open **Test Explorer** in Visual Studio
2. Click **Run All Tests**

---

## 🗄️ Database

- Uses **EF Core In-Memory Database**
- No external setup required
- Data resets when the application stops

Designed for easy migration to:
- PostgreSQL
- SQL Server
- Other relational databases

---

## ▶️ Running the Application

### Prerequisites
- .NET 6 SDK
- Visual Studio 2022 or later

### Steps
1. Clone the repository
2. Open `TodoApp.sln`
3. Set `TodoApp.Api` as the startup project
4. Run the application

Swagger UI will open automatically.

---

## 📌 Notes

- No frontend UI is included
- API-first design intended for extension
- Can be integrated with Angular / React frontend

---

## 👤 Author

**Yeswin Chintapalli**  
Software Developer | .NET Core | Backend Systems
