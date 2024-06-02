namespace GraphQL.StarWars.Schemas.Characters;

using GraphQL.StarWars.Repositories;
using GraphQL.StarWars.Repositories.Responses;
using GraphQL.StarWars.Schemas.Films;
using GraphQL.StarWars.Schemas.Planets;
using GraphQL.StarWars.Schemas.Species;
using GraphQL.StarWars.Schemas.Starships;
using GraphQL.StarWars.Schemas.Vehicles;

[ExtendObjectType(typeof(Character))]
public class CharacterExtension(
    [Service] IStarWarsRepository<FilmApiResponse> films,
    [Service] IStarWarsRepository<PlanetApiResponse> planets,
    [Service] IStarWarsRepository<SpeciesApiResponse> species,
    [Service] IStarWarsRepository<StarshipApiResponse> starships,
    [Service] IStarWarsRepository<VehicleApiResponse> vehicles)
{
    public async Task<Planet?> GetHomeworldAsync(
        [Parent] Character parent,
        CancellationToken ctx)
    {
        if (parent.HomeworldId is null)
        {
            return null;
        }

        var response = await planets.GetByIdAsync((int)parent.HomeworldId, ctx);

        if (response is null)
        {
            return null;
        }

        return Planet.MapFrom(response);
    }

    public async Task<IEnumerable<Film>> GetFilmsAsync(
        [Parent] Character parent,
        CancellationToken ctx)
    {
        var queue = parent.FilmIds
            .Select(id => films.GetByIdAsync(id, ctx));

        var responses = await Task.WhenAll(queue);

        var list = new List<Film>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(Film.MapFrom(e));
            }
        }

        return list;
    }

    public async Task<IEnumerable<Species>> GetSpeciesAsync(
        [Parent] Character parent,
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

    public async Task<IEnumerable<Starship>> GetStarshipsAsync(
        [Parent] Character parent,
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
        [Parent] Character parent,
        CancellationToken ctx)
    {
        var queue = parent.VehicleIds.Select(id => vehicles.GetByIdAsync(id, ctx));
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
}
