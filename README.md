# Rira Company – CRUD API Task (Clean Architecture)

This repository contains my solution for a **CRUD API task** assigned by **Rira Company**. The project is implemented using **C#** and **.NET**, following **Clean Architecture** principles to ensure a maintainable, testable, and scalable structure.

## ✅ Overview

The application exposes CRUD operations through a service-oriented design, with clear separation of concerns between the domain, application use-cases, infrastructure, and delivery layers. Data persistence is implemented using **Entity Framework Core** with **Code First** migrations. Communication is also provided via **gRPC**, using **Protocol Buffers** for strongly-typed contracts and efficient payloads.

## 🧱 Architecture

The solution is structured using **Clean Architecture**, typically organized into:

- **Domain Layer:** Core business entities, value objects, and domain rules
- **Application Layer:** Use cases, interfaces, DTOs, validation, and business workflows
- **Infrastructure Layer:** EF Core implementation, repositories, migrations, external services
- **WebApis Layer:** API endpoints and gRPC services (transport layer)

This approach keeps business logic independent from frameworks and infrastructure details.

## 🛠️ Tech Stack

- **Language:** C#
- **Platform:** .NET (Core)
- **Architecture:** Clean Architecture
- **ORM:** Entity Framework Core
- **Data Modeling: Code First + Migrations
- **Database:** SQL Server
- **Communication:** gRPC
- **Contracts:** Protocol Buffers (`.proto`)

## ✨ Key Features

-
- Full **CRUD** operations for the task entity/entities
- **EF Core Code First** modeling with migrations
-
