namespace GraphQL.Schemas.StarWars.Starships;

using GraphQL.Repositories.StarWars.Films;
using GraphQL.Repositories.StarWars.People;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.People;

[ExtendObjectType(typeof(StarshipSchema))]
public class StarshipExtension(
    [Service] IFilmRepository films,
    [Service] IPersonRepository people)
{
    public async Task<IEnumerable<FilmSchema>> GetFilmsAsync(
        [Parent] StarshipSchema parent,
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

    public async Task<IEnumerable<PersonSchema>> GetPilotsAsync(
        [Parent] StarshipSchema parent,
        CancellationToken ctx)
    {
        var pilotQueue = parent.PilotIds.Select(id => people.GetByIdAsync(id, ctx));
        var pilotResponses = await Task.WhenAll(pilotQueue);

        var pilotList = new List<PersonSchema>();

        foreach (var e in pilotResponses)
        {
            if (e is not null)
            {
                pilotList.Add(PersonSchema.MapFrom(e));
            }
        }

        return pilotList;
    }
}
