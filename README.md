# ToDo Backend

## Description

A RESTful API for managing todo tasks, built with .NET 9, Clean Architecture, CQRS, and MediatR. Supports user authentication, task management with customizable types, and dashboard statistics. The application allows users to create, update, and track their tasks with different statuses and categories.

## Features

- **User Management**: Registration, login, and JWT-based authentication
- **Task Management**: Full CRUD operations for todo tasks
- **Task Status Tracking**: Support for Pending, In Progress, and Completed statuses
- **Task Types**: Customizable task categories with color coding
- **Dashboard Statistics**: Overview of task completion and user activity
- **Pagination**: Efficient data retrieval with pagination support
- **Validation**: Comprehensive input validation using FluentValidation
- **Logging**: Structured logging with Serilog
- **API Versioning**: Versioned API endpoints
- **Docker Support**: Containerized deployment with Docker Compose

## Architecture Overview

The project follows Clean Architecture principles, separating concerns into distinct layers:

- **Domain Layer** (`ToDo-backend.Domain`): Contains core business entities (TodoTask, TaskType, User), value objects, domain logic, and abstractions. Defines the business rules and invariants.

- **Application Layer** (`ToDo-backend.Application`): Implements use cases using the CQRS pattern with MediatR. Contains commands, queries, handlers, DTOs, and validation logic. This layer orchestrates business operations without depending on external frameworks.

- **Infrastructure Layer** (`ToDo-backend.Infrastructure`): Provides implementations for data access (Entity Framework Core), authentication (JWT), external services, and cross-cutting concerns like logging and caching.

- **Web.API Layer** (`ToDo-backend.Web.API`): ASP.NET Core Web API project with controllers, routing, middleware, and API-specific configurations. Acts as the entry point for HTTP requests.

## Technologies Used

- **.NET 9.0**: Latest .NET runtime with C# 12 features
- **ASP.NET Core Web API**: Framework for building RESTful APIs
- **Entity Framework Core 9.0**: ORM for database operations
- **PostgreSQL**: Relational database for data persistence
- **MediatR 13.0**: Library for implementing CQRS pattern
- **FluentValidation 12.0**: Validation library for request models
- **JWT (JSON Web Tokens)**: For secure authentication
- **Serilog 4.3.0**: Structured logging framework
- **Docker & Docker Compose**: Containerization and orchestration
- **Asp.Versioning**: API versioning support

## Getting Started

### Prerequisites

- .NET 9.0 SDK (download from [Microsoft](https://dotnet.microsoft.com/download/dotnet/9.0))
- Docker and Docker Compose
- Git

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/ToDo-backend.git
   cd ToDo-backend
   ```

2. **Configure environment variables**:
   The `.env` file contains default configuration. You can modify it as needed:
   ```env
   # General
   DOCKER_REGISTRY=
   PROJECT_NAME=ToDoApp

   # Environment ASP.NET
   ASPNETCORE_ENVIRONMENT=Development

   # API
   API_HTTP_PORT=4040
   API_HTTPS_PORT=4041

   # DB Main
   POSTGRES_DB=todo
   POSTGRES_USER=postgres
   POSTGRES_PASSWORD=postgres*
   POSTGRES_PORT=5432
   ```

3. **Run the application**:
   ```bash
   docker-compose up --build
   ```

   The API will be available at:
   - HTTP: `http://localhost:4040`
   - HTTPS: `https://localhost:4041`

### Usage

1. **Register a new user**:
   ```bash
   curl -X POST http://localhost:4040/api/users/register \
     -H "Content-Type: application/json" \
     -d '{"email": "user@example.com", "password": "securepassword123"}'
   ```

2. **Login to get JWT token**:
   ```bash
   curl -X POST http://localhost:4040/api/users/login \
     -H "Content-Type: application/json" \
     -d '{"email": "user@example.com", "password": "securepassword123"}'
   ```

3. **Use the JWT token** for authenticated requests by including it in the Authorization header:
   ```
   Authorization: Bearer <your-jwt-token>
   ```

## API Endpoints Overview

All endpoints return JSON responses. Authenticated endpoints require a valid JWT token in the Authorization header.

### Authentication Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/users/register` | Register a new user account | No |
| POST | `/api/users/login` | Authenticate user and get access token | No |
| GET | `/api/users/whoami` | Get current authenticated user information | Yes |

### Todo Tasks Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/todotasks` | Get all tasks (paginated) | Yes |
| GET | `/api/todotasks/my` | Get current user's tasks with optional filters | Yes |
| GET | `/api/todotasks/{id}` | Get specific task by ID | Yes |
| POST | `/api/todotasks` | Create a new task | Yes |
| PUT | `/api/todotasks/{id}` | Update an existing task | Yes |
| PUT | `/api/todotasks/{id}/complete` | Mark task as completed | Yes |
| PUT | `/api/todotasks/{id}/inprogress` | Mark task as in progress | Yes |
| PUT | `/api/todotasks/{id}/pending` | Mark task as pending | Yes |
| DELETE | `/api/todotasks/{id}` | Delete a task | Yes |

**Query Parameters for `/api/todotasks/my`**:
- `page` (int): Page number (default: 1)
- `pageSize` (int): Items per page (default: 10)
- `taskTypeId` (int?): Filter by task type
- `status` (string?): Filter by status (Pending/InProgress/Completed)
- `sortByLastModifiedAscending` (bool?): Sort order

### Task Types Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/tasktypes` | Get all task types | Yes |
| GET | `/api/tasktypes/my` | Get current user's task types | Yes |
| GET | `/api/tasktypes/{id}` | Get specific task type by ID | Yes |
| POST | `/api/tasktypes` | Create a new task type | Yes |
| PUT | `/api/tasktypes/{id}` | Update an existing task type | Yes |
| DELETE | `/api/tasktypes/{id}` | Delete a task type | Yes |

### Dashboard Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/dashboard/stats` | Get dashboard statistics | Yes |

### Example API Calls

**Create a Task Type**:
```bash
curl -X POST http://localhost:4040/api/tasktypes \
  -H "Authorization: Bearer <your-token>" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Work",
    "colorHex": "#FF5733"
  }'
```

**Create a Todo Task**:
```bash
curl -X POST http://localhost:4040/api/todotasks \
  -H "Authorization: Bearer <your-token>" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Complete project documentation",
    "description": "Write comprehensive README and API docs",
    "taskTypeId": 1
  }'
```

**Get User's Tasks**:
```bash
curl -X GET "http://localhost:4040/api/todotasks/my?page=1&pageSize=5&status=Pending" \
  -H "Authorization: Bearer <your-token>"
```

## Development Notes

### Project Structure
```
ToDo-backend/
├── ToDo-backend.Domain/          # Domain entities and business logic
├── ToDo-backend.Application/     # CQRS commands, queries, and handlers
├── ToDo-backend.Infrastructure/  # Data access and external services
├── ToDo-backend.Web.API/         # ASP.NET Core Web API controllers
├── docker-compose.yml            # Docker orchestration
└── README.md
```

### Running Migrations
To apply database migrations during development:
```bash
cd ToDo-backend.Infrastructure
dotnet ef database update
```

### Testing
Run tests from the root directory:
```bash
dotnet test
```

### Logging
The application uses Serilog for structured logging. Logs are configured to output to console and can be extended to write to files or external services.

### API Versioning
The API supports versioning. Current version is v1.0. Use the `api-version` header or query parameter to specify the version.

### Security
- Passwords are hashed using secure hashing algorithms
- JWT tokens have configurable expiration
- All sensitive endpoints require authentication
- Input validation prevents common security vulnerabilities

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

For questions or issues, please open an issue on the GitHub repository.