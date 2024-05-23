namespace GraphQL.Schemas.StarWars.Species;

using GraphQL.Repositories.StarWars.Films;
using GraphQL.Repositories.StarWars.People;
using GraphQL.Repositories.StarWars.Planets;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.People;
using GraphQL.Schemas.StarWars.Planets;

[ExtendObjectType(typeof(SpeciesSchema))]
public class SpeciesExtension(
    [Service] IFilmRepository films,
    [Service] IPersonRepository people,
    [Service] IPlanetRepository planets)
{
    public async Task<PlanetSchema?> GetHomeworldAsync(
        [Parent] SpeciesSchema parent,
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

    public async Task<IEnumerable<PersonSchema>> GetPeopleAsync(
        [Parent] SpeciesSchema parent,
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
        [Parent] SpeciesSchema parent,
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
