namespace GraphQL.Schemas.StarWars.Starships;

using GraphQL.Repositories.StarWars.Starships;

[ExtendObjectType("Query")]
public class StarshipQuery([Service] IStarshipRepository starships)
{
    public async Task<IEnumerable<StarshipSchema>?> GetStarshipsAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await starships.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return null;
        }

        // Map and resolve
        return response.Results.Select(StarshipSchema.MapFrom);
    }

    public async Task<StarshipSchema?> GetStarshipAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await starships.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return StarshipSchema.MapFrom(response);
    }
}
