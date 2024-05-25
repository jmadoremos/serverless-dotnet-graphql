namespace GraphQL.Schemas.StarWars.Films;

using GraphQL.Repositories.StarWars.Characters;
using GraphQL.Repositories.StarWars.Planets;
using GraphQL.Repositories.StarWars.Species;
using GraphQL.Repositories.StarWars.Starships;
using GraphQL.Repositories.StarWars.Vehicles;
using GraphQL.Schemas.StarWars.Characters;
using GraphQL.Schemas.StarWars.Planets;
using GraphQL.Schemas.StarWars.Species;
using GraphQL.Schemas.StarWars.Starships;
using GraphQL.Schemas.StarWars.Vehicles;

[ExtendObjectType(typeof(FilmSchema))]
public class FilmExtension(
    [Service] ICharacterRepository people,
    [Service] IPlanetRepository planets,
    [Service] ISpeciesRepository species,
    [Service] IStarshipRepository starships,
    [Service] IVehicleRepository vehicles)
{
    public async Task<IEnumerable<CharacterSchema>> GetCharactersAsync(
        [Parent] FilmSchema parent,
        CancellationToken ctx)
    {
        var queue = parent.PersonIds.Select(id => people.GetByIdAsync(id, ctx));
        var responses = await Task.WhenAll(queue);

        var list = new List<CharacterSchema>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(CharacterSchema.MapFrom(e));
            }
        }

        return list;
    }

    public async Task<IEnumerable<PlanetSchema>> GetPlanetsAsync(
        [Parent] FilmSchema parent,
        CancellationToken ctx)
    {
        var queue = parent.PlanetIds.Select(id => planets.GetByIdAsync(id, ctx));
        var responses = await Task.WhenAll(queue);

        var list = new List<PlanetSchema>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(PlanetSchema.MapFrom(e));
            }
        }

        return list;
    }

    public async Task<IEnumerable<StarshipSchema>> GetStarshipsAsync(
        [Parent] FilmSchema parent,
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
        [Parent] FilmSchema parent,
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

    public async Task<IEnumerable<SpeciesSchema>> GetSpeciesAsync(
        [Parent] FilmSchema parent,
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
}
