# GeminiAPI Aspire

[![.NET 8](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![.NET Aspire](https://img.shields.io/badge/.NET%20Aspire-Enabled-purple.svg)](https://learn.microsoft.com/dotnet/aspire/)
[![Release](https://img.shields.io/github/v/release/abc15018045126/genimiapi-Aspire?include_prereleases)](https://github.com/abc15018045126/genimiapi-Aspire/releases)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

**GeminiAPI Aspire** is a cloud-ready, distributed AI chat application built with **.NET 8** and **.NET Aspire**. It features a modern Blazor Interactive Server frontend for interacting with Google Gemini and OpenAI-compatible API endpoints, complete with MongoDB conversation persistence, history control, parameter tuning, and built-in distributed telemetry.

---

## 🌟 Key Features

- **.NET Aspire Orchestration**: Centralized service discovery, lifecycle management, and observability dashboard powered by `AspireApp2.AppHost`.
- **Interactive Blazor Frontend**:
  - Full-featured chat interface (`ChatGPT.razor`) with real-time UI updates.
  - Multi-level debug logging and diagnostics output.
  - Automatic prompt sanitation and newline normalization for resilient API payload formatting.
  - Configurable chat history limits to manage context window and token usage.
- **Dynamic AI Parameter Tuning**:
  - Adjustable `Temperature`, `Top-P`, and `Top-K`.
  - Customizable System Prompts for persona injection.
  - Flexible endpoint switching between Google Gemini and OpenAI-compatible gateways.
- **MongoDB Persistence**:
  - Store and retrieve conversation history and chat logs directly in MongoDB collections.
- **Cloud-Ready Observability**:
  - Out-of-the-box OpenTelemetry metrics, distributed tracing, and health checks configured via `AspireApp2.ServiceDefaults`.
  - Fault tolerance and resilience policies integrated via Polly.

---

## 🏗️ Architecture & Project Structure

```text
genimiapi-Aspire/
├── AspireApp2.AppHost/          # .NET Aspire orchestration host
├── AspireApp2.ApiService/       # Backend REST API service with resilient endpoints
├── AspireApp2.ServiceDefaults/  # Shared OpenTelemetry, health checks, & resilience defaults
├── AspireApp2.Web/              # Blazor Web frontend (UI & AI client logic)
│   └── Components/
│       └── Pages/
│           └── ChatGPT.razor    # Primary AI chat and configuration interface
└── AspireApp2.sln               # Solution file
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or higher.
- [.NET Aspire Workload](https://learn.microsoft.com/dotnet/aspire/fundamentals/setup-tooling):
  ```bash
  dotnet workload install aspire
  ```
- [Docker Desktop](https://www.docker.com/) (recommended for containerized Aspire resources).
- A valid Google Gemini API Key or OpenAI-compatible endpoint key.
- (Optional) Running instance of [MongoDB](https://www.mongodb.com/).

### Running Locally

1. **Clone the repository**:
   ```bash
   git clone https://github.com/abc15018045126/genimiapi-Aspire.git
   cd genimiapi-Aspire
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Launch with .NET Aspire**:
   ```bash
   dotnet run --project AspireApp2.AppHost/AspireApp2.AppHost.csproj
   ```

4. **Access the Dashboard**:
   - Follow the dashboard URL output in the terminal console (e.g. `http://localhost:15xxx`).
   - Launch the `webfrontend` service from the dashboard to start interacting with the AI chat client.

---

## ⚙️ Configuration

In the Blazor web client UI:
1. Provide your **Gemini / OpenAI API Key** and base **API URL**.
2. Set your **MongoDB connection string** and database name for session persistence.
3. Configure **System Prompt**, **History Limits**, and generation hyperparameters as needed.

---

## 📦 Building for Release

To compile a release build locally:

```bash
dotnet publish -c Release -o ./publish
```

---

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.
