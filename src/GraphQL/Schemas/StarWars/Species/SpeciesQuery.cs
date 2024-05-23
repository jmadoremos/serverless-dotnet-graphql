namespace GraphQL.Schemas.StarWars.Species;

using GraphQL.Repositories.StarWars.Species;
using System.Collections.Generic;

[ExtendObjectType("Query")]
public class SpeciesQuery([Service] ISpeciesRepository species)
{
    public async Task<IEnumerable<SpeciesSchema>?> GetSpeciesAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await species.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return null;
        }

        // Map to schema and resolve
        return response.Results.Select(SpeciesSchema.MapFrom);
    }

    public async Task<SpeciesSchema?> GetSpeciesByIdAsync(
        [GraphQLType(typeof(IdType))] int id,
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
        return SpeciesSchema.MapFrom(response);
    }
}
