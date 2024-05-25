namespace GraphQL.Schemas.StarWars.Vehicles;

using GraphQL.Repositories.StarWars.Films;
using GraphQL.Repositories.StarWars.Characters;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.Characters;

[ExtendObjectType(typeof(VehicleSchema))]
public class VehicleExtension(
    [Service] IFilmRepository films,
    [Service] ICharacterRepository people)
{
    public async Task<IEnumerable<FilmSchema>> GetFilmsAsync(
        [Parent] VehicleSchema parent,
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

    public async Task<IEnumerable<CharacterSchema>> GetPilotsAsync(
        [Parent] VehicleSchema parent,
        CancellationToken ctx)
    {
        var pilotQueue = parent.PilotIds.Select(id => people.GetByIdAsync(id, ctx));
        var pilotResponses = await Task.WhenAll(pilotQueue);

        var pilotList = new List<CharacterSchema>();

        foreach (var e in pilotResponses)
        {
            if (e is not null)
            {
                pilotList.Add(CharacterSchema.MapFrom(e));
            }
        }

        return pilotList;
    }
}
