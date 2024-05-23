namespace GraphQL.Schemas.StarWars.People;

using GraphQL.Repositories.StarWars.Films;
using GraphQL.Repositories.StarWars.Planets;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.Planets;

[ExtendObjectType(typeof(PersonSchema))]
public class PersonExtension(
    [Service] IFilmRepository films,
    [Service] IPlanetRepository planets)
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
        var filmQueue = parent.FilmIds.Select(id => films.GetByIdAsync(id, ctx));
        var filmResponses = await Task.WhenAll(filmQueue);

        var filmList = new List<FilmSchema>();

        foreach (var e in filmResponses)
        {
            if (e is not null)
            {
                filmList.Add(FilmSchema.MapFrom(e));
            }
        }

        return filmList;
    }
}
