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

| Project            | Layer          | Responsibility                                       |
| :----------------- | :------------- | :--------------------------------------------------- |
| **Api**            | Presentation   | Minimal API endpoints, Dependency Injection setup.   |
| **Application**    | Core           | Use cases, DTOs, Interfaces/Abstractions.            |
| **Domain**         | Core           | Enterprise Logic, Entities.                          |
| **Infrastructure** | Infrastructure | EF Core DB Context, Repositories, External services. |

### Dependency Flow

`Api` -> `Application` -> `Domain`  
`Infrastructure` -> `Application` & `Domain`

---

## 📂 Folder Structure

The project follows a standard Clean Architecture directory structure:

```text
CleanMinimalApi/
├── CleanMinimalApi.Domain/
│   └── Entities/
├── CleanMinimalApi.Application/
│   ├── Abstractions/
│   │   └── Persistence/
│   ├── Dtos/
│   └── Features/
│       └── Products/
├── CleanMinimalApi.Infrastructure/
│   ├── Persistence/
│   ├── Repositories/
│   └── DependencyInjection/
└── CleanMinimalApi.Api/
    ├── Endpoints/
    └── DependencyInjection/
```
