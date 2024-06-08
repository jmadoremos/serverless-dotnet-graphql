# GraphQL for .NET Core - Database

## Create GraphQL.Database project

1. Create a new project using *Lambda ASP.NET Core Web API* template.

```sh
dotnet new serverless.AspNetCoreWebAPI --name GraphQL.Database --output .
```

2. Associate the project to the solution.

```sh
dotnet sln src/ServerlessDotNetGraphQL.sln add src/GraphQL.Database
```

3. Install package dependencies.

```sh
dotnet add src/GraphQL.Database package Newtonsoft.Json

dotnet add src/GraphQL.Database package Microsoft.EntityFrameworkCore.Tools

dotnet add src/GraphQL.Database package Aspire.Npgsql.EntityFrameworkCore.PostgreSQL

dotnet add src/GraphQL.Database package HotChocolate.AspNetCore

dotnet add src/GraphQL.Database package HotChocolate.Data

dotnet add src/GraphQL.Database package HotChocolate.Data.EntityFramework

dotnet add src/GraphQL.Database package ErrorOr
```

4. Create the folder structure as follows. Delete the files and folders inside the _src/GraphQL.Database_ folder that are not shown below.


```sh
GraphQL.Database
|_ DataLoaders/
    |_ AttendeeByIdBatchDataLoader.cs
    |_ SessionByIdBatchDataLoader.cs
    |_ SpeakerByIdBatchDataLoader.cs
    |_ TrackByIdBatchDataLoader.cs
|_ Exceptions/
    |_ GraphQLException.cs
    |_ SessionNotFoundException.cs
    |_ SessionSpeakerExistsException.cs
    |_ SessionSpeakerNotFoundException.cs
    |_ SessionTitleTakenException.cs
    |_ TrackNameTakenException.cs
    |_ TrackNotFoundException.cs
    |_ UsernameTakenException.cs
    |_ UserNotFoundException.cs
|_ Repositories/
    |_ Attendees/
        |_ AttendeeModel.cs
        |_ AttendeeModelInput.cs
        |_ AttendeeRepository.cs
        |_ IAttendeeRepository.cs
    |_ SessionAttendees/
        |_ ISessionAttendeeRepository.cs
        |_ SessionAttendeeModel.cs
        |_ SessionAttendeeModelInput.cs
        |_ SessionAttendeeRepository.cs
    |_ Sessions/
        |_ ISessionRepository.cs
        |_ SessionModel.cs
        |_ SessionModelInput.cs
        |_ SessionRepository.cs
    |_ SessionSpeakers/
        |_ ISessionSpeakerRepository.cs
        |_ SessionSpeakerModel.cs
        |_ SessionSpeakerModelInput.cs
        |_ SessionSpeakerRepository.cs
    |_ Speakers/
        |_ ISpeakerRepository.cs
        |_ SpeakerModel.cs
        |_ SpeakerModelInput.cs
        |_ SpeakerRepository.cs
    |_ Tracks/
        |_ ITrackRepository.cs
        |_ TrackModel.cs
        |_ TrackModelInput.cs
        |_ TrackRepository.cs
|_ Schemas/
    |_ Attendees/
        |_ AddAttendeeInput.cs
        |_ Attendee.cs
        |_ AttendeeExtension.cs
        |_ AttendeeMutation.cs
        |_ AttendeeQuery.cs
        |_ UpdateAttendeeInput.cs
    |_ Sessions/
        |_ AddRemoveSessionAttendeeInput.cs
        |_ AddRemoveSessionSpeakerInput.cs
        |_ AddSessionInput.cs
        |_ Session.cs
        |_ SessionExtension.cs
        |_ SessionMutation.cs
        |_ SessionQuery.cs
        |_ UpdateSessionInput.cs
    |_ Speakers/
        |_ AddSpeakerInput.cs
        |_ Speaker.cs
        |_ SpeakerExtension.cs
        |_ SpeakerMutation.cs
        |_ SpeakerQuery.cs
        |_ UpdateSpeakerInput.cs
    |_ Tracks/
        |_ AddTrackInput.cs
        |_ Track.cs
        |_ TrackExtension.cs
        |_ TrackMutation.cs
        |_ TrackQuery.cs
        |_ UpdateTrackInput.cs
|_ ApplicationDbContext.cs
|_ appsettings.Development.json
|_ appsettings.json
|_ aws-lambda-tools-default.json
|_ GraphQL.Database.csproj
|_ LambdaEntryPoint.cs
|_ LocalEntryPoint.cs
|_ Readme.md
|_ serverless.template
|_ Startup.cs
```

5. Update the contents of the following default files:

    * _src/GraphQL.StarWars/appsettings.Development.json_

    > Note: Port number is set to 5002 (HTTP) and 5003 (HTTPS).

    ```json
    {
        "AWS": {
            "Region": "us-east-1"
        },
        "AllowedHosts": "*",
        "Aspire": {
            "Npgsql": {
                "EntityFrameworkCore": {
                    "PostgreSQL": {
                        "DbContextPooling": true,
                        "DisableHealthChecks": true,
                        "DisableTracing": true
                    }
                }
            }
        },
        "ConnectionStrings": {
            "GraphQL": "User ID=postgre;Password=postgre;Host=localhost;Port=5432;Database=GraphQL;Pooling=true;Connection Lifetime=0;"
        },
        "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft.AspNetCore": "Warning"
            }
        },
        "Urls": "http://localhost:5004;https://localhost:5005"
    }
    ```

    * _src/GraphQL.StarWars/appsettings.json_

    > Note: Port number is set to 5002 (HTTP) and 5003 (HTTPS).

    ```json
    {
        "AWS": {
            "Region": "us-east-1"
        },
        "AllowedHosts": "*",
        "Aspire": {
            "Npgsql": {
                "EntityFrameworkCore": {
                    "PostgreSQL": {
                        "DbContextPooling": true,
                        "DisableHealthChecks": true,
                        "DisableTracing": true
                    }
                }
            }
        },
        "ConnectionStrings": {
            "GraphQL": "User ID=postgre;Password=postgre;Host=localhost;Port=5432;Database=GraphQL;Pooling=true;Connection Lifetime=0;"
        },
        "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft.AspNetCore": "Warning"
            }
        },
        "Urls": "http://localhost:5004;https://localhost:5005"
    }
    ```
