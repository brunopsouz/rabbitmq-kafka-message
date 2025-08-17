# RabbitMQ and Apache Kafka Messages
![](https://img.shields.io/badge/-RabbitMQ-333333?style=flat&logo=visual-studio-code&logoColor=007ACC)
![](https://img.shields.io/badge/-Apache%20Kafka-333333?style=flat&logo=visual-studio-code&logoColor=007ACC)
## Scalable Messaging Application with Repository Pattern
This project is a scalable version of a messaging application built using the `Repository Pattern` to ensure clean architecture and maintainability. It demonstrates how to integrate and switch between two powerful messaging services — RabbitMQ and Apache Kafka — showcasing the flexibility and scalability of the system.

## Key features:
- Repository Pattern for decoupled data access
- RabbitMQ integration for traditional message queuing
- Kafka integration for high-throughput event streaming
- Easily switch between messaging providers
- Designed for scalability and extensibility

## Architecture Overview:
- [Architecture](https://github.com/brunopsouz/rabbitmq-kafka-message/blob/main/STRUCTURE.md)

## Technologies Used
- .NET 
- RabbitMQ
- Apache Kafka
- Docker & Docker Compose
- Repository Pattern
- Clean Architecture principles

## Getting Started:
- Clone the repository:
```bash
https://github.com/brunopsouz/rabbitmq-kafka-message.git
```
- Up the Docker containers :
```bash
docker-compose up -d
```
- Open the Manage NuGet Packages menu and verify that RabbitMQ.Client or Apache are installed:
```bash
dotnet add package RabbitMQ.Client
```
```
dotnet add package Confluent.Kafka
```
- Make sure both projects are configured as Startup Projects:
```bash
Sln > Configure Startup Project > Multiple startup projects > Both Action: Start
```
- Define Dependency Injection in the program whose service is being used:
```csharp
builder.Services.AddTransient<IMessageService, MessageService>();
//builder.Services.AddTransient<IMessageRepository, RabbitMessageRepository>(); //RabbitMQ Repository
builder.Services.AddTransient<IMessageRepository, KafkaMessageRepository>(); // Kafka Repository
```
- Run:
```bash
dotnet run
```
- RabbitMQ: Access UI http://localhost:15672 (guest/guest).
- Apache Kafka: Download the Offset Explorer 3.0 and set up connection:
```
General
Cluster name: kafka
Bootstrap servers: localhost:9092
Kafka Cluster Version: latest

Zookeeper:
Host: localhost
Port: 2181
Chroot path: /
```
