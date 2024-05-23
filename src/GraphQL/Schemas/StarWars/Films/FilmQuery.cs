namespace GraphQL.Schemas.StarWars.Films;

using GraphQL.Repositories.StarWars.Films;

[ExtendObjectType("Query")]
public class FilmQuery([Service] IFilmRepository films)
{
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

    public async Task<FilmSchema?> GetFilmAsync(
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
