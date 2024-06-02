namespace GraphQL.WebAPI.Schemas.StarWars.Starships;

using GraphQL.WebAPI.Schemas.StarWars.Films;
using GraphQL.WebAPI.Schemas.StarWars.Characters;
using GraphQL.WebAPI.Repositories.StarWars;
using GraphQL.WebAPI.Repositories.StarWars.Responses;

[ExtendObjectType(typeof(Starship))]
public class StarshipExtension(
    [Service] IStarWarsRepository<CharacterApiResponse> characters,
    [Service] IStarWarsRepository<FilmApiResponse> films)
{
    public async Task<IEnumerable<Film>> GetFilmsAsync(
        [Parent] Starship parent,
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

    public async Task<IEnumerable<Character>> GetPilotsAsync(
        [Parent] Starship parent,
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