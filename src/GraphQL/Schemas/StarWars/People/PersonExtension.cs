namespace GraphQL.Schemas.StarWars.People;

using GraphQL.Repositories.StarWars.Films;
using GraphQL.Repositories.StarWars.Planets;
using GraphQL.Repositories.StarWars.Starships;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.Planets;
using GraphQL.Schemas.StarWars.Starships;

[ExtendObjectType(typeof(PersonSchema))]
public class PersonExtension(
    [Service] IFilmRepository films,
    [Service] IPlanetRepository planets,
    [Service] IStarshipRepository starships)
{
    public async Task<PlanetSchema?> GetHomeworldAsync(
        [Parent] PersonSchema parent,
        CancellationToken ctx)
    {
        var response = await planets.GetByIdAsync(parent.HomeworldId, ctx);

        if (response is null)
        {
            return null;
        }

        return PlanetSchema.MapFrom(response);
    }

    public async Task<IEnumerable<FilmSchema>?> GetFilmsAsync(
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

    public async Task<IEnumerable<StarshipSchema>?> GetStarshipsAsync(
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
}
