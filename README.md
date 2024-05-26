# serverless-dotnet-graphql

A Serverless Framework boilerplate for .NET that serves a GraphQL server

## Initial setup

1. Create a new solution inside *src* folder.

```sh
dotnet new sln --name ServerlessDotNetGraphQL --output src
```

2. Create a new project using *Lambda ASP.NET Core Web API* template.

```sh
dotnet new serverless.AspNetCoreWebAPI --name GraphQL --output .
```

3. Associate the project to the solution.

```sh
dotnet sln src/ServerlessDotNetGraphQL.sln add src/GraphQL
```

4. Install package dependencies.

```sh
dotnet add src/GraphQL package Microsoft.EntityFrameworkCore.Tools

dotnet add src/GraphQL package Aspire.Npgsql.EntityFrameworkCore.PostgreSQL

dotnet add src/GraphQL package HotChocolate.AspNetCore

dotnet add src/GraphQL package HotChocolate.Data.EntityFramework

dotnet add src/GraphQL package Newtonsoft.Json
```
