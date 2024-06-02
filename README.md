# serverless-dotnet-graphql

A Serverless Framework boilerplate for .NET that serves a GraphQL server

## Table of Contents

[1. Initial setup](#initial-setup)

[2. Create the Star Wars project](docs/starWars/01-create-graphql-starwars-project.md)

[3. Create the database project](docs/database/01-create-graphql-database-project.md)

[4. Create the gateway project](docs/gateway/01-create-graphql-gateway-project.md)

## Initial setup

1. Create a new solution inside *src* folder.

```sh
dotnet new sln --name ServerlessDotNetGraphQL --output src
```

2. Install tool dependencies.

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

3. Create the following files and their contents.

    * _.vscode/launch.json_

    > Note: These configurations are required to build the projects and run them in debugging mode.

    ```json
    {
        "configurations": [
            {
                "name": "C#: GraphQL.StarWars Debug",
                "type": "dotnet",
                "request": "launch",
                "projectPath": "${workspaceFolder}/src/GraphQL.StarWars/GraphQL.StarWars.csproj",
                "serverReadyAction": {
                    "action": "openExternally",
                    "pattern": "\\bNow listening on:\\s+https?://\\S",
                    "uriFormat": "http://localhost:5002/graphql/",
                    "killOnServerStop": true
                }
            },
            {
                "name": "C#: GraphQL.Gateway Debug",
                "type": "dotnet",
                "request": "launch",
                "projectPath": "${workspaceFolder}/src/GraphQL.Gateway/GraphQL.Gateway.csproj",
                "serverReadyAction": {
                    "action": "openExternally",
                    "pattern": "\\bNow listening on:\\s+https?://\\S",
                    "uriFormat": "http://localhost:5000/graphql/",
                    "killOnServerStop": true
                }
            },
            {
                "name": "C#: GraphQL.Database Debug",
                "type": "dotnet",
                "request": "launch",
                "projectPath": "${workspaceFolder}/src/GraphQL.Database/GraphQL.Database.csproj",
                "serverReadyAction": {
                    "action": "openExternally",
                    "pattern": "\\bNow listening on:\\s+https?://\\S",
                    "uriFormat": "http://localhost:5004/graphql/",
                    "killOnServerStop": true
                }
            }
        ]
    }
    ```

    * _.vscode/settings.json_

    > Note: These configurations are required by `mtxr.sqltools` extension to connect to the local database instance.

    ```json
    {
        "sqltools.connections": [
            {
                "previewLimit": 50,
                "server": "localhost",
                "port": 5432,
                "driver": "PostgreSQL",
                "name": "localhost",
                "database": "GraphQL",
                "username": "postgre",
                "password": "postgre"
            }
        ]
    }
    ```
