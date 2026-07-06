# eShop Microservices

A learning-oriented e-commerce backend built with a **microservices architecture** on **.NET 8**. Each service owns its own database (database-per-service) and communicates over REST (external) and gRPC (internal).

## Architecture

- **Vertical Slice Architecture + CQRS** — each feature is a self-contained folder with an `*Endpoint.cs` (route) and a `*Handler.cs` (command/query handler).
- **Carter** — minimal API endpoint definitions.
- **MediatR** — command/query pipeline between endpoints and handlers.
- **BuildingBlocks** — shared library (behaviors, exceptions, CQRS abstractions) referenced by every service.
- **Polyglot persistence** — each service picks the data store that fits its needs.

## Services

| Service          | Type | Data store              | Ports (http/https) |
|------------------|------|-------------------------|--------------------|
| Catalog.API      | REST | Postgres (via Marten)   | 6000 / 6060        |
| Basket.API       | REST | Postgres + Redis        | 6001 / 6061        |
| Discount.Grpc    | gRPC | Postgres (via EF Core)  | 6002 / 6062        |

- **Marten** — document DB over Postgres for the Catalog service.
- **Redis** (`distributedcache`) — distributed cache for Basket.
- **EF Core** (Npgsql) — relational access for Discount; migrations run on startup.
- Internal service-to-service calls (e.g. Basket → Discount) use **gRPC**; external clients use **REST**.

## Tech stack

.NET 8 · ASP.NET Core · Carter · MediatR · Marten · EF Core (Npgsql) · gRPC · PostgreSQL · Redis · Docker Compose

## Getting started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Run the full stack

```bash
# Build and start Postgres, Redis and all APIs
docker compose -f src/docker-compose.yml -f src/docker-compose.override.yml up -d --build

# Stop the stack
docker compose -f src/docker-compose.yml down
```

### Run a single service during development

```bash
dotnet run --project src/Services/Catalog/Catalog.API
```

### Build the whole solution

```bash
dotnet build src/eshop-microservices.slnx
```

## Configuration & secrets

- Local secrets go through **.NET User Secrets** (`dotnet user-secrets`); production uses environment variables / a secret manager.
- The `postgres/postgres` credentials are for **local development only**.
- Never hardcode connection strings, passwords, or API keys in code or `appsettings.json`.

## License

Distributed under the MIT License. See [LICENSE](LICENSE) for details.