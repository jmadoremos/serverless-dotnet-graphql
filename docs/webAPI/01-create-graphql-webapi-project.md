# GraphQL for .NET Core - Web APIs

## Create GraphQL.WebAPI project

1. Create a new project using *Lambda ASP.NET Core Web API* template.

```sh
dotnet new serverless.AspNetCoreWebAPI --name GraphQL.WebAPI --output .
```

2. Associate the project to the solution.

```sh
dotnet sln src/ServerlessDotNetGraphQL.sln add src/GraphQL.WebAPI
```

3. Install package dependencies.

```sh
dotnet add src/GraphQL.WebAPI package Newtonsoft.Json

dotnet add src/GraphQL.WebAPI package HotChocolate.AspNetCore
```

4. Create the folder structure as follows. Delete the files and folders inside the _src/GraphQL.WebAPI_ folder that are not shown below.

```sh
GraphQL.WebAPI
|_ Extensions/
    |_ UriExtension.cs
|_ Repositories/
    |_ StarWars/
        |_ Responses/
            |_ CharacterApiResponse.cs
            |_ FilmApiResponse.cs
            |_ PlanetApiResponse.cs
            |_ SpeciesApiResponse.cs
            |_ StarshipApiResponse.cs
            |_ SwapiResponse.cs
            |_ SwapiResponseList.cs
            |_ VehicleApiResponse.cs
        |_ IStarWarsRepository.cs
        |_ StarWarsRepository.cs
|_ Schemas/
    |_ StarWars/
        |_ Characters/
            |_ CharacterExtension.cs
            |_ Character.cs
        |_ Films/
            |_ FilmExtension.cs
            |_ Film.cs
        |_ Planets/
            |_ PlanetExtension.cs
            |_ Planet.cs
        |_ Species/
            |_ SpeciesExtension.cs
            |_ Species.cs
        |_ Starships/
            |_ StarshipExtension.cs
            |_ Starship.cs
        |_ Vehicles/
            |_ VehicleExtension.cs
            |_ Vehicle.cs
|_ Services/
    |_ ISwapiService.cs
    |_ SwapiService.cs
|_ Types/
    |_ SwapiUrl.cs
|_ appsettings.Development.json
|_ appsettings.json
|_ aws-lambda-tools-default.json
|_ GraphQL.WebAPI.csproj
|_ LambdaEntryPoint.cs
|_ LocalEntryPoint.cs
|_ Readme.md
|_ serverless.template
|_ Startup.cs
```

5. Update the contents of the following default files:

    * _src/GraphQL.WebAPI/appsettings.Development.json_

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
        "Urls": "http://localhost:5002;https://localhost:5003"
    }
    ```

    * _src/GraphQL.WebAPI/appsettings.json_

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
        "Urls": "http://localhost:5002;https://localhost:5003"
    }
    ```

6. Create an extension of `System.Uri` class to extract Star Wars API unique identifiers for the resources from the URL.

> Location: src/GraphQL.WebAPI/Extensions/UriExtension.cs

```cs
namespace GraphQL.WebAPI.Extensions;

using System.Globalization;

public static class UriExtension
{
    public static int ExtractSwapiId(this Uri @this)
    {
        var last = @this.Segments.LastOrDefault();

        if (last is null)
        {
            return 0;
        }

        if (last.EndsWith('/'))
        {
            last = last.Remove(last.Length - 1);
        }

        return Convert.ToInt16(last, CultureInfo.InvariantCulture);
    }
}
```
