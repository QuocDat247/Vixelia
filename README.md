# Vixelia

Vixelia is an AI-assisted desktop creative workspace focused on image editing, generation, enhancement, and future video workflows.

> Status: Early development, `0.1.0-alpha.1`. Not production-ready.

## Vision

Vixelia combines a familiar desktop editing experience with natural-language AI assistance.

The user interface is designed around familiar desktop productivity patterns, with an emphasis on clarity, discoverability, and a canvas-first workflow.

## Technology Stack

- C#
- .NET 10 LTS
- WPF
- ASP.NET Core Web API
- MVVM
- Entity Framework Core
- SQLite
- xUnit
- Git
- GitHub

## Solution Structure

```text
src/
├── Vixelia.Domain
├── Vixelia.Application
├── Vixelia.Contracts
├── Vixelia.Infrastructure
├── Vixelia.Infrastructure.AI
├── Vixelia.Api
└── Vixelia.Desktop

tests/
├── Vixelia.Tests.Unit
└── Vixelia.Tests.Integration
```

## Architecture

Vixelia follows a layered architecture with clear dependency boundaries.

- Domain contains core business concepts and rules.
- Application contains use cases and abstractions.
- Infrastructure contains persistence, filesystem, settings, and external implementations.
- Infrastructure.AI contains server-side AI provider adapters.
- Contracts contains API request and response models.
- Api exposes the backend HTTP interface.
- Desktop contains the WPF user interface.

AI provider secrets must never be embedded in the desktop application.

## Development

Restore dependencies:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

Run tests:

```bash
dotnet test
```

Run the API:

```bash
dotnet run --project src/Vixelia.Api
```

Run the desktop application:

```bash
dotnet run --project src/Vixelia.Desktop
```

## Current Roadmap

The first milestone focuses on:

- Desktop workspace
- Image import
- Non-destructive media versioning
- AI assistant
- Image generation and editing
- Project persistence
- Provider-independent AI integration

Video workflows are planned for a later milestone.

## Security

Secrets, AI provider API keys, credentials, and sensitive configuration must not be committed to source control or embedded in the desktop application.

## License

No public license has been selected yet.