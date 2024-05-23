namespace GraphQL.Schemas.StarWars.Planets;

using GraphQL.Repositories.StarWars.Films;
using GraphQL.Repositories.StarWars.People;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.People;

[ExtendObjectType(typeof(PlanetSchema))]
public class PlanetExtension(
    [Service] IPersonRepository people,
    [Service] IFilmRepository films)
{
    public async Task<IEnumerable<PersonSchema>> GetResidentsAsync(
        [Parent] PlanetSchema parent,
        CancellationToken ctx)
    {
        var queue = parent.PersonIds.Select(id => people.GetByIdAsync(id, ctx));
        var responses = await Task.WhenAll(queue);

        var list = new List<PersonSchema>();

        foreach (var e in responses)
        {
            if (e is not null)
            {
                list.Add(PersonSchema.MapFrom(e));
            }
        }

        return list;
    }

    public async Task<IEnumerable<FilmSchema>> GetFilmsAsync(
        [Parent] PlanetSchema parent,
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
}
