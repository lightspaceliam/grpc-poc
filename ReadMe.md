# GrpcPoc - Proto3, .NET Core 3.1^

After a couple of months of working with a .NET Core flavoured version of gRPC, I decided to explore a little deeper to get a better understanding of how everything worked and try to resolve other issues such as: 

1. How to support nullable Model/Protobuf properties without adding custom  validation
2. Reuse the .proto/Protobuf contract across multiple .NET projects 

## Nullable Support

Issue, I have a database table with nullable columns. When I derive the data into my gRPC service I don’t want to have to escape those values manually. I would prefer the framework handle this as I would prefer to reduce complexity and reduce the requirement for extra unit testing. Also when the data is inserted or updated into my database, I want to be able to store null instead of a default value such as a zero, empty string, … Similar to C#, Protobuf does support nullable types:

**C#**
```c#
public int? MyNallableIntegerProperty { get; set; } 
```
**Protobuf**
```proto3
google.protobuf.Int32Value MyNallableIntegerProperty = 1;
```

Reference [Nullable Types](https://docs.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/protobuf-data-types#nullable-types)

## Duplicate .proto Contracts

One reason for adding a new project and putting functionality in it is to re-use it across multiple projects. In doing so I initially needed to duplicate the .proto contract across multiple projects. A work colleague recently discovered a better solution:

**Service GrpcPoc.PersonService/GrpcPoc.PersonService.csproj**
```xml
<ItemGroup>
    <Protobuf 
      Include="..\Protos\GrpcPerson.proto"
      Link="Protos\GrpcPerson.proto"
      GrpcServices="Server" />
</ItemGroup>
```
**Client GrpcPoc.Api/GrpcPoc.Api.csproj**
```xml
<ItemGroup>
    <Protobuf
      Include="..\Protos\GrpcPerson.proto"
      Link="Protos\GrpcPerson.proto"
      GrpcServices="Client" />
</ItemGroup>
```
The .proto contract is no longer duplicated and sits in a directory GrpcPoc/Protos

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
