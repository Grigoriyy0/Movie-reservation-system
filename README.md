# Movie-reservation-system

Данный проект является примером использования паттерна CQRS + MediatR в ASP.NET. В качесте примера выбрана система бронирования фильмов

.NET 8 + ASP.NET Core, PostgreSQL, EFCore, MediatR, CQRS + FluentValidation

---------------------------------------------------------

This project demonstrates a practical implementation of the CQRS pattern using MediatR in an ASP.NET Core 8 application for a movie ticket reservation system. The architecture emphasizes separation of concerns between command and query operations.


Backend:
  .NET 8
  ASP.NET Core Web API
  Entity Framework Core 8
  MediatR 12
  FluentValidation
  
Database:
  PostgreSQL
  Entity Framework Core Migrations

Architectural Patterns:
  CQRS (Command Query Responsibility Segregation)
  Clean Architecture (partial implementation)
  Repository Pattern (via EF Core)

Getting Started
  Prerequisites
    .NET 8 SDK
    PostgreSQL 15+
    Docker (optional)

Installation
  Clone the repository
  Configure connection string in appsettings.json

Run migrations:
  dotnet ef database update

Running the Application
  dotnet run --project src/MovieReservationSystem 
