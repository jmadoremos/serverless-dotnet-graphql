namespace GraphQL.WebAPI.Schemas.Species;

using GraphQL.WebAPI.Repositories;
using GraphQL.WebAPI.Repositories.Responses;

[ExtendObjectType("Query")]
public class SpeciesQuery(
    [Service] IStarWarsRepository<SpeciesApiResponse> species)
{
    [UsePaging]
    [GraphQLDescription("A list of species in Star Wars Universe.")]
    public async Task<IEnumerable<Species>> GetSpeciesAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await species.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map to schema and resolve
        return response.Results.Select(Species.MapFrom);
    }

    [GraphQLDescription("A species in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Species?> GetSpeciesByIdAsync(
        [ID(nameof(Species))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await species.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return Species.MapFrom(response);
    }
}
