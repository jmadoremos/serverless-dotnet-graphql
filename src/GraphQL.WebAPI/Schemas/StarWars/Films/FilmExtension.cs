namespace GraphQL.WebAPI.Schemas.StarWars.Films;

using GraphQL.WebAPI.Repositories.StarWars;
using GraphQL.WebAPI.Repositories.StarWars.Responses;
using GraphQL.WebAPI.Schemas.StarWars.Characters;
using GraphQL.WebAPI.Schemas.StarWars.Planets;
using GraphQL.WebAPI.Schemas.StarWars.Species;
using GraphQL.WebAPI.Schemas.StarWars.Starships;
using GraphQL.WebAPI.Schemas.StarWars.Vehicles;

[ExtendObjectType(typeof(Film))]
public class FilmExtension(
    [Service] IStarWarsRepository<CharacterApiResponse> characters,
    [Service] IStarWarsRepository<PlanetApiResponse> planets,
    [Service] IStarWarsRepository<SpeciesApiResponse> species,
    [Service] IStarWarsRepository<StarshipApiResponse> starships,
    [Service] IStarWarsRepository<VehicleApiResponse> vehicles)
{
    public async Task<IEnumerable<Character>> GetCharactersAsync(
        [Parent] Film parent,
        CancellationToken ctx)
    {
        var queue = parent.PersonIds
            .Select(id => characters.GetByIdAsync(id, ctx));

        var responses = await Task.WhenAll(queue);

        var list = new List<Character>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(Character.MapFrom(e));
            }
        }

        return list;
    }

    public async Task<IEnumerable<Planet>> GetPlanetsAsync(
        [Parent] Film parent,
        CancellationToken ctx)
    {
        var queue = parent.PlanetIds
            .Select(id => planets.GetByIdAsync(id, ctx));

        var responses = await Task.WhenAll(queue);

        var list = new List<Planet>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(Planet.MapFrom(e));
            }
        }

        return list;
    }

    public async Task<IEnumerable<Starship>> GetStarshipsAsync(
        [Parent] Film parent,
        CancellationToken ctx)
    {
        var queue = parent.StarshipIds
            .Select(id => starships.GetByIdAsync(id, ctx));

        var responses = await Task.WhenAll(queue);

        var list = new List<Starship>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(Starship.MapFrom(e));
            }
        }

        return list;
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesAsync(
        [Parent] Film parent,
        CancellationToken ctx)
    {
        var queue = parent.VehicleIds
            .Select(id => vehicles.GetByIdAsync(id, ctx));

        var responses = await Task.WhenAll(queue);

        var list = new List<Vehicle>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(Vehicle.MapFrom(e));
            }
        }

        return list;
    }

    public async Task<IEnumerable<Species>> GetSpeciesAsync(
        [Parent] Film parent,
        CancellationToken ctx)
    {
        var queue = parent.SpeciesIds
            .Select(id => species.GetByIdAsync(id, ctx));

        var responses = await Task.WhenAll(queue);

        var list = new List<Species>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(Species.MapFrom(e));
            }
        }

        return list;
    }
}
