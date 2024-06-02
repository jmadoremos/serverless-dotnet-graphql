namespace GraphQL.StarWars.Schemas.Films;

using GraphQL.StarWars.Repositories;
using GraphQL.StarWars.Repositories.Responses;

[ExtendObjectType("Query")]
public class FilmQuery(
    [Service] IStarWarsRepository<FilmApiResponse> films)
{
    [UsePaging]
    [GraphQLDescription("A list of films in Star Wars Universe.")]
    public async Task<IEnumerable<Film>> GetFilmsAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await films.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map and resolve
        return response.Results
            .Select(Film.MapFrom);
    }

    [GraphQLDescription("A film in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Film?> GetFilmByIdAsync(
        [ID(nameof(Film))] int id,
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
        return Film
            .MapFrom(response);
    }
}
