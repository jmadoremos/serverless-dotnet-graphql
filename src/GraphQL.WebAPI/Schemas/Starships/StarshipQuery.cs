namespace GraphQL.WebAPI.Schemas.Starships;

using GraphQL.WebAPI.Repositories;
using GraphQL.WebAPI.Repositories.Responses;

[ExtendObjectType("Query")]
public class StarshipQuery(
    [Service] IStarWarsRepository<StarshipApiResponse> starships)
{
    [UsePaging]
    [GraphQLDescription("A list of starships in Star Wars Universe.")]
    public async Task<IEnumerable<Starship>> GetStarshipsAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await starships.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map and resolve
        return response.Results.Select(Starship.MapFrom);
    }

    [GraphQLDescription("A starship in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Starship?> GetStarshipByIdAsync(
        [ID(nameof(Starship))] int id,
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
        return Starship.MapFrom(response);
    }
}
