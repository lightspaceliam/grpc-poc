# gRPC POC - Proto3, .NET Core 3.1^

## Tech
- .NET Core 3.1^
- gRPC Proto3
- Entity Framework with Code First Migrations
- API, SQL

## Installation

Ensure you have the following installed:
* [.NET Core 3.1^](https://dotnet.microsoft.com/download)
* [SQL Server](https://www.microsoft.com/en-gb/sql-server/sql-server-downloads)
* [.NET Core Code First Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
* Clone this github repo
```
git clone https://github.com/lightspaceliam/grpc-poc.git
```
* Database migrations and seeding is already configured
* Visual Studio Code or Visual Studio 2019 - you will need to run both GrpcPoc.Api & GrpcPoc.PersonService concurrently

Handy hint, if you want to disable/stop auto migrations and or database seeding, got to /grpc-poc/GrpcPoc.PersonService/Program.cs and comment out:
```c#
webBuilder.MigrateDatabase();
webBuilder.SeedDatabase();
```

## Endpoints
- GET   https://localhost:5001/api/people
- GET   https://localhost:5001/api/people?maxRecords={maxRecords}
- POST  https://localhost:5001/api/people/new
- GET   https://localhost:5001/api/people/{id}

**POST Payload:**
```json
{
    "firstName": "Ned",
    "middleName": null,
    "lastName": "Flanders",
    "dateOfBirth": "1945-09-18T00:00:00Z"
}
```

## Resouces

**Official**
- [Manage Protobuf references with dotnet-grpc](https://docs.microsoft.com/en-us/aspnet/core/grpc/dotnet-grpc?view=aspnetcore-3.1)
- [gRPC services with C#](https://docs.microsoft.com/en-us/aspnet/core/grpc/basics?view=aspnetcore-3.1)
- [Create Protobuf messages for .NET apps](https://docs.microsoft.com/en-us/aspnet/core/grpc/protobuf?view=aspnetcore-3.1)
- [Protobuf scalar data types](https://docs.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/protobuf-data-types)

**Community**
- [.NET Implementing Microservices with gRPC and .NET Core 3.1 (Andrea Chiarelli)](https://auth0.com/blog/implementing-microservices-grpc-dotnet-core-3/)
