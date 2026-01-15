# Platform & Commands Microservices

A comprehensive .NET microservices project demonstrating modern distributed system patterns, communication protocols, and Kubernetes deployment. This project showcases a Platform Service and Commands Service that communicate through multiple channels: HTTP, gRPC, and asynchronous messaging via RabbitMQ.

## 📋 Project Overview

This tutorial-based project implements a real-world microservices architecture with:

- **Two Independent Services**: Platform Service and Commands Service
- **Multiple Communication Patterns**: Synchronous (HTTP/gRPC) and Asynchronous (RabbitMQ)
- **Containerized Deployment**: Docker and Kubernetes orchestration
- **Persistent Data**: MSSQL databases with Entity Framework Core
- **Service-to-Service Integration**: Event-driven architecture

## 🏗️ Architecture

### Services

#### **Platform Service**
Creates and manages platforms. Handles platform lifecycle and notifies other services of platform creation events.

**Key Features:**
- RESTful API for CRUD operations on platforms
- gRPC service for efficient synchronous communication with Commands Service
- Publishes platform creation events to RabbitMQ
- Synchronously communicates platform data via HTTP/gRPC to Commands Service

**Endpoints:**
- `GET /api/platforms/get-all` - Retrieve all platforms
- `GET /api/platforms/get-by-id/{id}` - Get platform by ID
- `POST /api/platforms/create` - Create a new platform
- `DELETE /api/platforms/delete/{id}` - Delete a platform
- gRPC: `Port 789` for GrpcPlatformService

#### **Commands Service**
Manages commands associated with platforms. Subscribes to platform events and maintains a synchronized copy of platform data.

**Key Features:**
- RESTful API for managing platform commands
- Listens for platform events via RabbitMQ
- Automatically syncs platform data when new platforms are created
- gRPC client for fetching platform data from Platform Service
- Hosted service for continuous RabbitMQ message subscription

**Endpoints:**
- `GET /api/c/platforms/{platformId}/commands` - Get all commands for a platform
- `GET /api/c/platforms/{platformId}/commands/{commandId}` - Get a specific command
- `POST /api/c/platforms/{platformId}/commands` - Create a command for a platform

### Data Models

#### Platform (Platform Service)
```csharp
public class Platform
{
    public int Id { get; set; }
    public string Name { get; set; }        // e.g., "Kubernetes"
    public string Publisher { get; set; }   // e.g., "CNCF"
    public string Cost { get; set; }        // e.g., "Free"
}
```

#### Command (Commands Service)
```csharp
public class Command
{
    public int Id { get; set; }
    public string HowTo { get; set; }           // e.g., "Deploy an app"
    public string CommandLine { get; set; }    // e.g., "kubectl apply -f app.yaml"
    public int PlatformId { get; set; }
    public Platform Platform { get; set; }
}
```

## 🔌 Communication Patterns

### 1. **HTTP Communication**
- Platform Service exposes REST endpoints for CRUD operations
- Commands Service calls Platform Service to sync new platform data

### 2. **gRPC Communication**
- Synchronous, high-performance communication between services
- Platform Service exposes gRPC service on port 789
- Commands Service uses gRPC client to fetch platform data
- Protocol buffer definition: `Protos/platforms.proto`

### 3. **Async Messaging (RabbitMQ)**
- Event-driven architecture for eventual consistency
- **Event Published**: `Platform_Published` event when a new platform is created
- **Event Subscriber**: Commands Service subscribes to platform events
- Decouples services for better resilience and scalability
- Handled by `MessageBusSubscriber` hosted service

## 🗄️ Database

Both services use Entity Framework Core with MSSQL Server:

- **Platform Service DB**: Stores platforms
- **Commands Service DB**: Stores commands and a synced copy of platforms

**Migrations:**
- Platform Service: `20260108204349_initialMigration`
- Commands Service: `20260109173505_InitalMigration`

## 🐳 Docker & Kubernetes Deployment

### Container Images
- `muhammedkb/platformservice:latest` - Platform Service container
- `muhammedkb/commandsservice:latest` - Commands Service container
- MSSQL databases deployed as separate containers

### Kubernetes Resources

Located in the `K8S/` directory:

- **Deployments**:
  - `platforms-depl.yaml` - Platform Service deployment with ClusterIP service (ports 80, 789)
  - `commands-depl.yaml` - Commands Service deployment with ClusterIP service (port 80)
  - `mssql-platform-depl.yaml` - MSSQL for Platform Service
  - `mssql-command-depl.yaml` - MSSQL for Commands Service
  - `rabbitmq-depl.yaml` - RabbitMQ message broker

- **Services**:
  - `platforms-np-srv.yaml` - NodePort service for external access to Platform Service
  - `ingress-srv.yaml` - Ingress controller for routing

- **Storage**:
  - `local-pvc.yaml` - Persistent volume for data persistence

### K8s Service Discovery
- Platform Service: `platform-clusterip-srv`
- Commands Service: `commands-clusterip-srv`
- Internal communication uses service names as DNS

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- Docker & Docker Compose (for local testing)
- Kubernetes cluster (minikube, Docker Desktop K8s, or cloud provider)
- MSSQL Server 2019+ (or use Docker image)
- RabbitMQ (or use Docker image)

### Local Development

#### 1. Clone and Restore Dependencies
```bash
cd PlatformCommands
dotnet restore PlatformService/PlatformService.csproj
dotnet restore CommandsService/CommandsService.csproj
```

#### 2. Database Setup
Update connection strings in `appsettings.json` or `appsettings.Development.json`:

```json
"DefaultConnection": "Server=localhost;Database=PlatformDb;User Id=sa;Password=YourPassword;"
```

#### 3. Run Migrations
```bash
# Platform Service
cd PlatformService
dotnet ef database update

# Commands Service
cd ../CommandsService
dotnet ef database update
```

#### 4. Start Services
```bash
# Terminal 1 - Platform Service
cd PlatformService
dotnet run

# Terminal 2 - Commands Service
cd CommandsService
dotnet run
```

### Kubernetes Deployment

#### 1. Build and Push Docker Images
```bash
# Build Platform Service
docker build -t yourusername/platformservice:latest ./PlatformService
docker push yourusername/platformservice:latest

# Build Commands Service
docker build -t yourusername/commandsservice:latest ./CommandsService
docker push yourusername/commandsservice:latest
```

Update image references in K8S manifests if using different image names.

#### 2. Deploy to Kubernetes
```bash
# Create persistent volumes
kubectl apply -f K8S/local-pvc.yaml

# Deploy databases and message broker
kubectl apply -f K8S/mssql-platform-depl.yaml
kubectl apply -f K8S/mssql-command-depl.yaml
kubectl apply -f K8S/rabbitmq-depl.yaml

# Deploy services
kubectl apply -f K8S/platforms-depl.yaml
kubectl apply -f K8S/commands-depl.yaml
kubectl apply -f K8S/platforms-np-srv.yaml

# Apply ingress routing
kubectl apply -f K8S/ingress-srv.yaml
```

#### 3. Verify Deployment
```bash
kubectl get pods
kubectl get svc
kubectl logs -f deployment/platforms-deployment
kubectl logs -f deployment/commands-deployment
```

## 📝 Testing the API

### Using REST Endpoints

#### Create a Platform
```bash
curl -X POST http://localhost:5000/api/platforms/create \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Docker",
    "publisher": "Docker Inc",
    "cost": "Free"
  }'
```

#### Get All Platforms
```bash
curl http://localhost:5000/api/platforms/get-all
```

#### Create a Command
```bash
curl -X POST http://localhost:5000/api/c/platforms/1/commands \
  -H "Content-Type: application/json" \
  -d '{
    "howTo": "Build an image",
    "commandLine": "docker build -t myapp:latest ."
  }'
```

#### Get Commands for Platform
```bash
curl http://localhost:5000/api/c/platforms/1/commands
```

### Using HTTP Files
- Platform Service: `PlatformService/PlatformService.http`
- Commands Service: `CommandsService/CommandsService.http`

Use VS Code REST Client extension or Postman to test.

## 📦 Project Structure

```
PlatformCommands/
├── PlatformService/                    # Platform Service
│   ├── Controllers/                    # API endpoints
│   ├── Models/                         # Domain models
│   ├── Dtos/                           # Data transfer objects
│   ├── Data/                           # DbContext and repositories
│   ├── Migrations/                     # EF Core migrations
│   ├── AsyncDataServices/              # RabbitMQ client
│   ├── SyncDataServices/               # HTTP and gRPC clients
│   ├── Extensions/                     # DI and configuration
│   ├── Protos/                         # Protocol buffer definitions
│   └── Program.cs
│
├── CommandsService/                    # Commands Service
│   ├── Controllers/                    # API endpoints
│   ├── Models/                         # Domain models
│   ├── Dtos/                           # Data transfer objects
│   ├── Data/                           # DbContext and repositories
│   ├── Migrations/                     # EF Core migrations
│   ├── EventProcessing/                # Event handlers
│   ├── AsyncDataServices/              # RabbitMQ subscriber
│   ├── SyncDataServices/               # gRPC client
│   ├── Extensions/                     # DI and configuration
│   ├── Protos/                         # Protocol buffer definitions
│   └── Program.cs
│
├── K8S/                                # Kubernetes manifests
│   ├── platforms-depl.yaml
│   ├── commands-depl.yaml
│   ├── mssql-platform-depl.yaml
│   ├── mssql-command-depl.yaml
│   ├── rabbitmq-depl.yaml
│   ├── platforms-np-srv.yaml
│   ├── ingress-srv.yaml
│   └── local-pvc.yaml
│
└── PlatformCommands.sln                # Solution file
```

## 🔧 Technologies & Dependencies

### Core Framework
- .NET 9.0
- ASP.NET Core
- Entity Framework Core 9.0.1

### Communication
- gRPC 2.76.0
- Grpc.AspNetCore (Server)
- Grpc.Net.Client (Client)
- Protocol Buffers

### Messaging
- RabbitMQ.Client 7.2.0

### Data Access
- Microsoft.EntityFrameworkCore 9.0.1
- Microsoft.EntityFrameworkCore.SqlServer 9.0.1
- Microsoft.EntityFrameworkCore.InMemory 9.0.1 (development)

### Tools
- AutoMapper 12.0.1
- Grpc.Tools 2.76.0 (for proto code generation)

### Deployment
- Docker
- Kubernetes

## 🔄 Data Flow Example

### Creating a New Platform

1. **HTTP Request**: Client sends POST request to `POST /api/platforms/create`
2. **Platform Creation**: Platform Service creates platform in MSSQL database
3. **HTTP Sync**: Platform Service calls Commands Service via HTTP to sync platform data
4. **Async Event**: Platform Service publishes `Platform_Published` event to RabbitMQ
5. **Message Subscription**: Commands Service MessageBusSubscriber receives event
6. **Event Processing**: Platform data is extracted and stored in Commands Service database
7. **gRPC Fallback**: Commands Service can also call Platform Service via gRPC if needed

### Querying Commands

1. **HTTP Request**: Client sends GET to `/api/c/platforms/{platformId}/commands`
2. **Repository Query**: Commands Service queries local MSSQL database
3. **Response**: Returns all commands for the specified platform

## 📚 Learning Resources

This project is based on microservices tutorial concepts covering:
- Service-to-service communication (REST, gRPC)
- Event-driven architecture with message brokers
- Database per service pattern
- Containerization with Docker
- Orchestration with Kubernetes
- Entity Framework Core migrations
- Async/await patterns
- Dependency injection and configuration

## ⚙️ Configuration

### Environment-Specific Settings

- `appsettings.json` - Default configuration
- `appsettings.Development.json` - Development environment
- `appsettings.Production.json` - Production environment

### Key Configuration Areas

**Database Connections**:
```json
"ConnectionStrings": {
  "DefaultConnection": "connection_string_here"
}
```

**RabbitMQ Settings**:
- Host configuration in `Program.cs` extensions
- Default: `localhost` for development

**gRPC Ports**:
- Platform Service: `789`
- Configure in `launchSettings.json`

## 🐛 Troubleshooting

### Services Can't Connect
- Verify service DNS names in Kubernetes
- Check RabbitMQ connectivity with `rabbitmq-management` plugin
- Ensure databases are accessible

### Database Migration Issues
```bash
# Check pending migrations
dotnet ef migrations list

# Revert last migration
dotnet ef migrations remove
```

### gRPC Connection Failures
- Verify port 789 is exposed in K8S service
- Check protocol buffer compatibility
- Use `grpcurl` to test gRPC endpoints

### RabbitMQ Message Loss
- Verify persistence is enabled
- Check message queue configuration
- Ensure subscriber is running before publishing

## 📄 License

This is a tutorial-based project for educational purposes.

## 🤝 Contributing

This repository is for learning purposes. Feel free to fork, modify, and extend the project for your learning.

---

**Created**: Based on comprehensive microservices tutorial  
**Technologies**: .NET 9.0, ASP.NET Core, Entity Framework Core, gRPC, RabbitMQ, Kubernetes
