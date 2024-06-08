namespace GraphQL.StarWars.Schemas.Vehicles;

using GraphQL.StarWars.Repositories;
using GraphQL.StarWars.Repositories.Responses;
using GraphQL.StarWars.Schemas.Films;
using GraphQL.StarWars.Schemas.Characters;

[ExtendObjectType(typeof(Vehicle))]
public class VehicleExtension(
    [Service] IStarWarsRepository<CharacterApiResponse> characters,
    [Service] IStarWarsRepository<FilmApiResponse> films)
{
    [BindMember(nameof(Vehicle.FilmIds))]
    [GraphQLDescription("A list of films that this vehicle has appeared in.")]
    public async Task<IEnumerable<Film>> GetFilmsAsync(
        [Parent] Vehicle parent,
        CancellationToken ctx)
    {
        var filmQueue = parent.FilmIds
            .Select(id => films.GetByIdAsync(id, ctx));

        var filmResponses = await Task.WhenAll(filmQueue);

        var filmList = new List<Film>();

        foreach (var e in filmResponses)
        {
            if (e is not null)
            {
                filmList.Add(Film.MapFrom(e));
            }
        }

        return filmList;
    }

    [BindMember(nameof(Vehicle.PilotIds))]
    [GraphQLDescription("A list of pilots that this vehicle has been piloted by.")]
    public async Task<IEnumerable<Character>> GetPilotsAsync(
        [Parent] Vehicle parent,
        CancellationToken ctx)
    {
        var pilotQueue = parent.PilotIds
            .Select(id => characters.GetByIdAsync(id, ctx));

        var pilotResponses = await Task.WhenAll(pilotQueue);

        var pilotList = new List<Character>();

        foreach (var e in pilotResponses)
        {
            if (e is not null)
            {
                pilotList.Add(Character.MapFrom(e));
            }
        }

        return pilotList;
    }
}
