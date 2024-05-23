namespace GraphQL.Schemas.StarWars.People;

using GraphQL.Repositories.StarWars.Films;
using GraphQL.Repositories.StarWars.Planets;
using GraphQL.Repositories.StarWars.Species;
using GraphQL.Repositories.StarWars.Starships;
using GraphQL.Repositories.StarWars.Vehicles;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.Planets;
using GraphQL.Schemas.StarWars.Species;
using GraphQL.Schemas.StarWars.Starships;
using GraphQL.Schemas.StarWars.Vehicles;

[ExtendObjectType(typeof(PersonSchema))]
public class PersonExtension(
    [Service] IFilmRepository films,
    [Service] IPlanetRepository planets,
    [Service] ISpeciesRepository species,
    [Service] IStarshipRepository starships,
    [Service] IVehicleRepository vehicles)
{
    public async Task<PlanetSchema?> GetHomeworldAsync(
        [Parent] PersonSchema parent,
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

        return PlanetSchema.MapFrom(response);
    }

    public async Task<IEnumerable<FilmSchema>> GetFilmsAsync(
        [Parent] PersonSchema parent,
        CancellationToken ctx)
    {
        var queue = parent.FilmIds.Select(id => films.GetByIdAsync(id, ctx));
        var responses = await Task.WhenAll(queue);

        var list = new List<FilmSchema>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(FilmSchema.MapFrom(e));
            }
        }

        return list;
    }

    public async Task<IEnumerable<SpeciesSchema>> GetSpeciesAsync(
        [Parent] PersonSchema parent,
        CancellationToken ctx)
    {
        var queue = parent.SpeciesIds.Select(id => species.GetByIdAsync(id, ctx));
        var responses = await Task.WhenAll(queue);

        var list = new List<SpeciesSchema>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(SpeciesSchema.MapFrom(e));
            }
        }

        return list;
    }

    public async Task<IEnumerable<StarshipSchema>> GetStarshipsAsync(
        [Parent] PersonSchema parent,
        CancellationToken ctx)
    {
        var queue = parent.StarshipIds.Select(id => starships.GetByIdAsync(id, ctx));
        var responses = await Task.WhenAll(queue);

        var list = new List<StarshipSchema>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(StarshipSchema.MapFrom(e));
            }
        }

        return list;
    }

    public async Task<IEnumerable<VehicleSchema>> GetVehiclesAsync(
        [Parent] PersonSchema parent,
        CancellationToken ctx)
    {
        var queue = parent.VehicleIds.Select(id => vehicles.GetByIdAsync(id, ctx));
        var responses = await Task.WhenAll(queue);

        var list = new List<VehicleSchema>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(VehicleSchema.MapFrom(e));
            }
        }

        return list;
    }
}
