namespace GraphQL.WebAPI.Schemas.StarWars;

using GraphQL.WebAPI.Repositories.StarWars;
using GraphQL.WebAPI.Repositories.StarWars.Responses;
using GraphQL.WebAPI.Schemas.StarWars.Characters;
using GraphQL.WebAPI.Schemas.StarWars.Films;
using GraphQL.WebAPI.Schemas.StarWars.Planets;
using GraphQL.WebAPI.Schemas.StarWars.Starships;
using GraphQL.WebAPI.Schemas.StarWars.Vehicles;

[ExtendObjectType(typeof(StarWars))]
public class StarWarsExtension(
    [Service] IStarWarsRepository<CharacterApiResponse> characters,
    [Service] IStarWarsRepository<FilmApiResponse> films,
    [Service] IStarWarsRepository<PlanetApiResponse> planets,
    [Service] IStarWarsRepository<SpeciesApiResponse> species,
    [Service] IStarWarsRepository<StarshipApiResponse> starships,
    [Service] IStarWarsRepository<VehicleApiResponse> vehicles)
{
    [UsePaging]
    [GraphQLDescription("A list of people in Star Wars Universe.")]
    public async Task<IEnumerable<Character>> GetCharactersAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await characters.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map to schema and resolve
        return response.Results
            .Select(Character.MapFrom);
    }

    [GraphQLDescription("A person or character in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Character?> GetCharacterByIdAsync(
        [ID(nameof(Character))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await characters.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return Character.MapFrom(response);
    }

    [UsePaging]
    [GraphQLDescription("A list of films in Star Wars Universe.")]
    public async Task<IEnumerable<Film>> GetFilmsAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await films.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map and resolve
        return response.Results
            .Select(Film.MapFrom);
    }

    [GraphQLDescription("A film in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Film?> GetFilmByIdAsync(
        [ID(nameof(Film))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await films.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return Film
            .MapFrom(response);
    }

    [UsePaging]
    [GraphQLDescription("A list of planets in Star Wars Universe.")]
    public async Task<IEnumerable<Planet>> GetPlanetsAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await planets.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map and resolve
        return response.Results.Select(Planet.MapFrom);
    }

    [GraphQLDescription("A planet in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Planet?> GetPlanetByIdAsync(
        [ID(nameof(Planet))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await planets.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return Planet.MapFrom(response);
    }

    [UsePaging]
    [GraphQLDescription("A list of species in Star Wars Universe.")]
    public async Task<IEnumerable<Species.Species>> GetSpeciesAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await species.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map to schema and resolve
        return response.Results.Select(Species.Species.MapFrom);
    }

    [GraphQLDescription("A species in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Species.Species?> GetSpeciesByIdAsync(
        [ID(nameof(Species.Species))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await species.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return Species.Species.MapFrom(response);
    }

    [UsePaging]
    [GraphQLDescription("A list of starships in Star Wars Universe.")]
    public async Task<IEnumerable<Starship>> GetStarshipsAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await starships.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map and resolve
        return response.Results.Select(Starship.MapFrom);
    }

    [GraphQLDescription("A starship in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Starship?> GetStarshipByIdAsync(
        [ID(nameof(Starship))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await starships.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return Starship.MapFrom(response);
    }

    [UsePaging]
    [GraphQLDescription("A list of vehicle in Star Wars Universe.")]
    public async Task<IEnumerable<Vehicle>> GetVehiclesAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await vehicles.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map and resolve
        return response.Results.Select(Vehicle.MapFrom);
    }

    [GraphQLDescription("A vehicle in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Vehicle?> GetVehicleByIdAsync(
        [ID(nameof(Vehicle))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await vehicles.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return Vehicle.MapFrom(response);
    }
}
