# GraphQL for .NET Core - Gateway

## Create GraphQL.Gateway project

1. Create a new project using *Lambda ASP.NET Core Web API* template.

```sh
dotnet new serverless.AspNetCoreWebAPI --name GraphQL.Gateway --output .
```

2. Associate the project to the solution.

```sh
dotnet sln src/ServerlessDotNetGraphQL.sln add src/GraphQL.Gateway
```

3. Install package dependencies.

```sh
dotnet add src/GraphQL.Gateway package Newtonsoft.Json

dotnet add src/GraphQL.Gateway package HotChocolate.AspNetCore

dotnet add src/GraphQL.Gateway package HotChocolate.Stitching
```

4. Create the folder structure as follows. Delete the files and folders inside the _src/GraphQL.Gateway_ folder that are not shown below.

```sh
GraphQL.Gateway
|_ appsettings.Development.json
|_ appsettings.json
|_ aws-lambda-tools-default.json
|_ GraphQL.Gateway.csproj
|_ LambdaEntryPoint.cs
|_ LocalEntryPoint.cs
|_ Readme.md
|_ serverless.template
|_ Startup.cs
|_ WellKnownSchemaNames.cs
```

5. Update the contents of the following default files:

    * _src/GraphQL.Gateway/appsettings.Development.json_

    > Note: Port number is set to 5002 (HTTP) and 5003 (HTTPS).

    ```json
    {
        "AWS": {
            "Region": "us-east-1"
        },
        "AllowedHosts": "*",
        "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft.AspNetCore": "Warning"
            }
        },
        "Urls": "http://localhost:5000;https://localhost:5001"
    }
    ```

    * _src/GraphQL.Gateway/appsettings.json_

    > Note: Port number is set to 5002 (HTTP) and 5003 (HTTPS).

    ```json
    {
        "AWS": {
            "Region": "us-east-1"
        },
        "AllowedHosts": "*",
        "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft.AspNetCore": "Warning"
            }
        },
        "Urls": "http://localhost:5000;https://localhost:5001"
    }
    ```
