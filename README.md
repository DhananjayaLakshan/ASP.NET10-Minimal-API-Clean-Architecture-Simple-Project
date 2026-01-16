# Clean Architecture Minimal API (.NET 10)

This project demonstrates a **Clean Architecture** implementation using **.NET 10** and **Minimal APIs**. It utilizes **Entity Framework Core** with SQLite and adheres to strict dependency rules between the Domain, Application, Infrastructure, and API layers.

---

## 📋 Prerequisites

Before running or building this project, ensure you have the following installed:

1.  **.NET 10 SDK**
2.  **EF Core Tools** (Global installation):
    ```bash
    dotnet tool install --global dotnet-ef
    ```

---

## 🏗️ Architecture Overview

The solution is divided into four strictly defined projects:

| Project | Layer | Responsibility |
| :--- | :--- | :--- |
| **Api** | Presentation | Minimal API endpoints, Dependency Injection setup. |
| **Application** | Core | Use cases, DTOs, Interfaces/Abstractions. |
| **Domain** | Core | Enterprise Logic, Entities. |
| **Infrastructure** | Infrastructure | EF Core DB Context, Repositories, External services. |

### Dependency Flow
`Api` -> `Application` -> `Domain`  
`Infrastructure` -> `Application` & `Domain`

---
### 🚀 Setup Instructions
If you are recreating this project from scratch, follow the CLI commands below.

1. Create Solution & Projects
Initialize the solution and the four-layer projects:

mkdir CleanMinimalApi && cd CleanMinimalApi
dotnet new sln -n CleanMinimalApi

### 1. Create Solution & Projects
Initialize the solution and the four-layer projects:
##### Create projects
```bash
dotnet new web -n CleanMinimalApi.Api
dotnet new classlib -n CleanMinimalApi.Domain
dotnet new classlib -n CleanMinimalApi.Application
dotnet new classlib -n CleanMinimalApi.Infrastructure
```
#### Add projects to Solution
```bash
dotnet sln add CleanMinimalApi.Api/CleanMinimalApi.Api.csproj
dotnet sln add CleanMinimalApi.Domain/CleanMinimalApi.Domain.csproj
dotnet sln add CleanMinimalApi.Application/CleanMinimalApi.Application.csproj
dotnet sln add CleanMinimalApi.Infrastructure/CleanMinimalApi.Infrastructure.csproj
```
### 2. Configure Dependencies
Establish the reference directions to enforce the architecture:
#### Application depends on Domain
```bash
dotnet add CleanMinimalApi.Application/CleanMinimalApi.Application.csproj reference CleanMinimalApi.Domain/CleanMinimalApi.Domain.csproj
```
#### Infrastructure depends on Application and Domain
```bash
dotnet add CleanMinimalApi.Infrastructure/CleanMinimalApi.Infrastructure.csproj reference CleanMinimalApi.Application/CleanMinimalApi.Application.csproj
dotnet add CleanMinimalApi.Infrastructure/CleanMinimalApi.Infrastructure.csproj reference CleanMinimalApi.Domain/CleanMinimalApi.Domain.csproj
```
#### API depends on Application and Infrastructure
```bash
dotnet add CleanMinimalApi.Api/CleanMinimalApi.Api.csproj reference CleanMinimalApi.Application/CleanMinimalApi.Application.csproj
dotnet add CleanMinimalApi.Api/CleanMinimalApi.Api.csproj reference CleanMinimalApi.Infrastructure/CleanMinimalApi.Infrastructure.csproj
```
### 3. Install NuGet Packages
Infrastructure Layer (EF Core + SQLite):
```bash
dotnet add CleanMinimalApi.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add CleanMinimalApi.Infrastructure package Microsoft.EntityFrameworkCore.Sqlite
dotnet add CleanMinimalApi.Infrastructure package Microsoft.EntityFrameworkCore.Design
```
API Layer (OpenAPI + CORS):
```bash
dotnet add CleanMinimalApi.Api package Microsoft.AspNetCore.OpenApi
dotnet add CleanMinimalApi.Api package Swashbuckle.AspNetCore
```
### 🗄️ Database Setup
This project uses Code First migrations with SQLite.
#### 1. Create the initial migration:
```bash
dotnet ef migrations add InitialCreate \
  --project CleanMinimalApi.Infrastructure \
  --startup-project CleanMinimalApi.Api \
  --output-dir Persistence/Migrations
```
#### 2. Update the database: This command creates the app.db file in the API startup directory.
```bash
dotnet ef database update \
  --project CleanMinimalApi.Infrastructure \
  --startup-project CleanMinimalApi.Api
```

### ▶️ Running the Application
#### 1. Run the API:
```bash
dotnet run --project CleanMinimalApi.Api
```
### 2. Access Swagger UI: Once the application is running, open your browser to:
```bash
http://localhost:<port>/swagger
```
