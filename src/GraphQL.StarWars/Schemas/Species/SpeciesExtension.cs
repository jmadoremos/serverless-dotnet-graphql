namespace GraphQL.StarWars.Schemas.Species;

using GraphQL.StarWars.Repositories;
using GraphQL.StarWars.Repositories.Responses;
using GraphQL.StarWars.Schemas.Films;
using GraphQL.StarWars.Schemas.Characters;
using GraphQL.StarWars.Schemas.Planets;

[ExtendObjectType(typeof(Species))]
public class SpeciesExtension(
    [Service] IStarWarsRepository<CharacterApiResponse> characters,
    [Service] IStarWarsRepository<FilmApiResponse> films,
    [Service] IStarWarsRepository<PlanetApiResponse> planets)
{
    [BindMember(nameof(Species.HomeworldId))]
    [GraphQLDescription("A planet that this species originates from.")]
    public async Task<Planet?> GetHomeworldAsync(
        [Parent] Species parent,
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

    [BindMember(nameof(Species.PeopleIds))]
    [GraphQLDescription("A list of people that are a part of this species.")]
    public async Task<IEnumerable<Character>> GetPeopleAsync(
        [Parent] Species parent,
        CancellationToken ctx)
    {
        var queue = parent.PeopleIds
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

    [BindMember(nameof(Species.FilmIds))]
    [GraphQLDescription("A list of films that this species has appeared in.")]
    public async Task<IEnumerable<Film>> GetFilmsAsync(
        [Parent] Species parent,
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
