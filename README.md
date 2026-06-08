# 📋 Task Management System API

A RESTful API built with **ASP.NET Core 9** following **Clean Architecture** principles. It provides task management with subscription-based access control, user management, and JWT authentication.

---

## 🏗️ Architecture

The project follows **Clean Architecture** with 4 layers:

```
TaskManagementSystem/
├── TaskManagementSystem.API            # Presentation Layer (Controllers, Middleware, Extensions)
├── TaskManagementSystem.Application    # Application Layer (Services, DTOs, Interfaces, Validators)
├── TaskManagementSystem.Domain         # Domain Layer (Entities, Enums, Constants)
└── TaskManagementSystem.Infrastructure # Infrastructure Layer (Repositories, Persistence, Seeding)
```

| Layer | Responsibility |
|---|---|
| **API** | Controllers, middleware, DI registration, Swagger configuration |
| **Application** | Business logic (services), DTOs, interfaces, FluentValidation validators |
| **Domain** | Core entities (`ApplicationUser`, `TaskItem`, `UserSubscription`), enums |
| **Infrastructure** | EF Core repositories, `ApplicationDbContext`, database migrations, role seeding |

---

## ⚙️ Tech Stack

- **.NET 9** — Target framework
- **ASP.NET Core Identity** — User & role management
- **Entity Framework Core** — ORM with SQL Server
- **JWT Bearer Authentication** — Token-based auth (header + cookie)
- **FluentValidation** — Request validation
- **Swagger / Swashbuckle** — API documentation

---

## 🔐 Authentication

The API uses **JWT tokens** delivered via:
1. **Authorization header** — `Bearer <token>`
2. **HTTP-only cookie** — `jwt` (set automatically on login/register)

### Roles
| Role | Description |
|---|---|
| `Admin` | Full access — manage tasks, users, and subscriptions |
| `User` | Access tasks based on subscription level, update task status |

---

## 📡 API Endpoints

### Auth (`/api/auth`)

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/auth/register` | ❌ | Register a new user |
| `POST` | `/api/auth/login` | ❌ | Login and receive JWT |
| `GET` | `/api/auth/me` | ✅ | Get current user info from token |
| `POST` | `/api/auth/logout` | ❌ | Logout (clears JWT cookie) |

### Tasks (`/api/tasks`) — Admin Only

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `GET` | `/api/tasks` | ✅ | Get all tasks |
| `GET` | `/api/tasks/{id}` | ✅ | Get task by ID |
| `POST` | `/api/tasks` | 🔒 Admin | Create a new task |
| `PUT` | `/api/tasks/{id}` | 🔒 Admin | Update a task |
| `DELETE` | `/api/tasks/{id}` | 🔒 Admin | Delete a task |

### User Tasks (`/api/user-tasks`) — Authenticated Users

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `GET` | `/api/user-tasks/my-tasks` | ✅ | Get tasks matching user's subscription level |
| `PATCH` | `/api/user-tasks/{taskId}/status` | ✅ | Update a task's status |

### Subscriptions (`/api/subscriptions`) — Admin Only

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/subscriptions/assign` | 🔒 Admin | Assign a subscription to a user |
| `PUT` | `/api/subscriptions/{userId}` | 🔒 Admin | Update a user's subscription |
| `GET` | `/api/subscriptions` | 🔒 Admin | Get all subscriptions |
| `GET` | `/api/subscriptions/{userId}` | 🔒 Admin | Get subscription by user ID |

### Users (`/api/users`) — Admin Only

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `GET` | `/api/users` | 🔒 Admin | Get all users |
| `GET` | `/api/users/{id}` | 🔒 Admin | Get user by ID |

---

## 📊 Domain Model

### Enums

```
SubscriptionType: Basic (1) | Premium (2)
TaskStatus:       Pending (1) | Completed (2)
```

### Entities

```
ApplicationUser (IdentityUser<Guid>)
├── FullName
└── Subscription → UserSubscription?

TaskItem
├── Id, Title, Description
├── Status (TaskStatus)
└── RequiredSubscriptionLevel (SubscriptionType)

UserSubscription
├── Id, UserId
├── SubscriptionType
└── User → ApplicationUser
```

> **Access Rule:** A user can only see tasks where `RequiredSubscriptionLevel <= User's SubscriptionType`.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/) (or SQL Server Express)

### 1. Clone the repository

```bash
git clone https://github.com/mohamed-osamaaa/TaskManagementSystem.git
cd TaskManagementSystem
```

### 2. Configure the database

Update the connection string in `TaskManagementSystem.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "JWT": {
    "Issuer": "TaskManagementAPI",
    "Audience": "TaskManagementAPI",
    "SecretKey": "YourSuperSecretKeyAtLeast32Characters"
  }
}
```

### 3. Apply migrations

```bash
dotnet ef database update --project TaskManagementSystem.Infrastructure --startup-project TaskManagementSystem.API
```

### 4. Run the application

```bash
dotnet run --project TaskManagementSystem.API
```

The API will be available at `https://localhost:5001` and Swagger UI at `https://localhost:5001/swagger`.

---

## 📁 Project Structure

```
TaskManagementSystem.API/
├── Controllers/
│   ├── AuthController.cs
│   ├── TasksController.cs
│   ├── UserTasksController.cs
│   ├── SubscriptionsController.cs
│   └── UsersController.cs
├── Extensions/
│   └── ServiceExtensions.cs
├── Middleware/
│   └── GlobalExceptionMiddleware.cs
└── Program.cs

TaskManagementSystem.Application/
├── Common/
│   └── ApiResponse.cs
├── DTOs/
│   ├── LoginDto.cs
│   ├── RegisterDto.cs
│   ├── AuthResponseDto.cs
│   ├── UserDto.cs
│   ├── TaskItemDto.cs
│   ├── UpdateTaskStatusDto.cs
│   ├── AssignSubscriptionDto.cs
│   ├── UpdateSubscriptionDto.cs
│   └── SubscriptionDto.cs
├── Interfaces/
│   ├── IAuthService.cs / IAuthRepository.cs
│   ├── ITasksService.cs / ITasksRepository.cs
│   ├── IUserTasksService.cs / IUserTasksRepository.cs
│   ├── ISubscriptionsService.cs / ISubscriptionsRepository.cs
│   ├── IUsersService.cs / IUsersRepository.cs
│   └── IJwtService.cs
├── Services/
│   ├── AuthService.cs
│   ├── TasksService.cs
│   ├── UserTasksService.cs
│   ├── SubscriptionsService.cs
│   └── UsersService.cs
└── Validators/
    ├── LoginDtoValidator.cs
    ├── RegisterDtoValidator.cs
    ├── TaskItemDtoValidator.cs
    ├── UpdateTaskStatusDtoValidator.cs
    ├── AssignSubscriptionDtoValidator.cs
    └── UpdateSubscriptionDtoValidator.cs

TaskManagementSystem.Domain/
├── Entities/
│   ├── ApplicationUser.cs
│   ├── TaskItem.cs
│   └── UserSubscription.cs
└── Enums/
    ├── TaskStatus.cs
    └── SubscriptionType.cs

TaskManagementSystem.Infrastructure/
├── Persistence/
│   └── ApplicationDbContext.cs
├── Repositories/
│   ├── AuthRepository.cs
│   ├── TasksRepository.cs
│   ├── UserTasksRepository.cs
│   ├── SubscriptionsRepository.cs
│   └── UsersRepository.cs
├── Services/
│   └── JwtService.cs
├── Seeding/
│   └── RoleSeeder.cs
└── Migrations/
```

---

## 📜 API Response Format

All endpoints return a consistent response:

```json
{
  "success": true,
  "message": "Operation completed successfully.",
  "data": { },
  "errors": []
}
```

---

## 📄 License

This project is open-source and available under the [MIT License](LICENSE).
