namespace GraphQL.StarWars.Schemas.Planets;

using GraphQL.StarWars.Repositories;
using GraphQL.StarWars.Repositories.Responses;
using GraphQL.StarWars.Schemas.Films;
using GraphQL.StarWars.Schemas.Characters;

[ExtendObjectType(typeof(Planet))]
public class PlanetExtension(
    [Service] IStarWarsRepository<CharacterApiResponse> characters,
    [Service] IStarWarsRepository<FilmApiResponse> films)
{
    [BindMember(nameof(Planet.ResidentIds))]
    [GraphQLDescription("A list of residents that live on this planet.")]
    public async Task<IEnumerable<Character>> GetResidentsAsync(
        [Parent] Planet parent,
        CancellationToken ctx)
    {
        var queue = parent.ResidentIds
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

    [BindMember(nameof(Planet.FilmIds))]
    [GraphQLDescription("A list of films that this planet has appeared in.")]
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
