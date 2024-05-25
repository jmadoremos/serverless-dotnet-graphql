namespace GraphQL.Schemas.StarWars.Films;

using GraphQL.Repositories.StarWars.Films;

[ExtendObjectType("Query")]
public class FilmQuery([Service] IFilmRepository films)
{
    [GraphQLDescription("A list of films in Star Wars Universe.")]
    public async Task<IEnumerable<FilmSchema>?> GetFilmsAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await films.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return null;
        }

        // Map and resolve
        return response.Results.Select(FilmSchema.MapFrom);
    }

    [GraphQLDescription("A film in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<FilmSchema?> GetFilmByIdAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await films.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return FilmSchema.MapFrom(response);
    }
}
