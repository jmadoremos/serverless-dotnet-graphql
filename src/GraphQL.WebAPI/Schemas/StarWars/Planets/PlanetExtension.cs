namespace GraphQL.WebAPI.Schemas.StarWars.Planets;

using GraphQL.WebAPI.Repositories.StarWars;
using GraphQL.WebAPI.Schemas.StarWars.Films;
using GraphQL.WebAPI.Schemas.StarWars.Characters;
using GraphQL.WebAPI.Repositories.StarWars.Responses;

[ExtendObjectType(typeof(Planet))]
public class PlanetExtension(
    [Service] IStarWarsRepository<CharacterApiResponse> characters,
    [Service] IStarWarsRepository<FilmApiResponse> films)
{
    public async Task<IEnumerable<Character>> GetResidentsAsync(
        [Parent] Planet parent,
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

    public async Task<IEnumerable<Film>> GetFilmsAsync(
        [Parent] Planet parent,
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
}
