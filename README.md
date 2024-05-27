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
dotnet add src/GraphQL package Newtonsoft.Json

dotnet add src/GraphQL package Microsoft.EntityFrameworkCore.Tools

dotnet add src/GraphQL package Aspire.Npgsql.EntityFrameworkCore.PostgreSQL

dotnet add src/GraphQL package HotChocolate.AspNetCore

dotnet add src/GraphQL package HotChocolate.Data.EntityFramework
```

5. Install tool dependencies.

    * To create from scratch,

    ```sh
    # Create local manifest (see: .config/dotnet-tools.json)
    dotnet new tool-manifest

    # Install and add Amazon.Lambda.Tools to local manifest
    dotnet tool install --local Amazon.Lambda.Tools

    # Install and add .NET test tools to local manifest
    dotnet tool install --local Amazon.Lambda.TestTool-8.0

    # Install and add .NET Entity Framework tools to local manifest
    dotnet tool install --local dotnet-ef
    ```

    * To install existing local manifest,

    ```sh
    dotnet tool restore
    ```
