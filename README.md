# User and Tasks API

## Overview

This is an **ASP.NET Core Web API** project for managing users and tasks. It supports **CRUD operations**, **task filtering**, **Swagger documentation**, and optional **JWT authentication**.

---

## Features

* **Users**

  * Create, Read, Update, Delete (CRUD)
* **Tasks**

  * Create, Read, Update, Delete (CRUD)
  * Filter tasks:

    * Expired tasks
    * Active tasks
    * Tasks by specific date
    * Tasks by assigned user
* **Swagger UI**

  * Explore and test API endpoints
* **Authentication (Optional)**

  * JWT (JSON Web Token) authentication for secured endpoints

---

## Models

### User

| Property | Type   | Description          |
| -------- | ------ | -------------------- |
| ID       | int    | Primary Key          |
| Username | string | User's username      |
| Email    | string | User's email address |
| Password | string | User's password      |

### Task

| Property    | Type     | Description                  |
| ----------- | -------- | ---------------------------- |
| ID          | int      | Primary Key                  |
| Title       | string   | Task title                   |
| Description | string   | Task description             |
| Assignee    | int      | User ID assigned to the task |
| DueDate     | DateTime | Task due date                |

---

## Setup & Installation

1. **Clone the repository**

```bash
git clone https://github.com/<your-username>/User-and-Tasks-API.git
cd User-and-Tasks-API
```

2. **Configure the database**

* Edit `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=TaskDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

* Apply EF Core migrations:

```bash
dotnet ef database update
```

3. **Run the API**

```bash
dotnet run
```

4. **Swagger UI**

* Open browser:

```
http://localhost:5209
```

* Explore endpoints and test API requests.

---

## Authentication (Optional)

* JWT is implemented for secure endpoints.
* To test:

  1. Create a user via `/api/users` endpoint.
  2. Log in to receive a JWT token.
  3. Use the token to access protected endpoints by adding:

```
Authorization: Bearer <your_token_here>
```

---

## Testing Endpoints

* **Get all tasks**

```
GET /api/tasks
```

* **Get expired tasks**

```
GET /api/tasks/expired
```

* **Get tasks by date**

```
GET /api/tasks/by-date/{yyyy-MM-dd}
```

* **Get tasks by assignee**

```
GET /api/tasks/by-assignee/{userId}
```

* **Create task**

```
POST /api/tasks
Body: {
  "title": "Finish API test",
  "description": "Test CRUD endpoints",
  "assignee": 1,
  "dueDate": "2025-09-01T12:00:00"
}
```

---

## Tools & Technologies

* ASP.NET Core 7
* Entity Framework Core
* SQL Server
* Swagger (Swashbuckle)
* JWT Authentication
* Visual Studio / VS Code

---

## Repository Structure

```
/Controllers       -> API controllers for Users and Tasks
/Models            -> User and Task models
/Repositories      -> Repository pattern for database access
/Data              -> DbContext and database configuration
/Program.cs        -> API configuration and startup
```

---

## Notes

* Filtering endpoints are **public**.
* CRUD endpoints are **secured with JWT**.
* Swagger UI allows testing without Postman.


