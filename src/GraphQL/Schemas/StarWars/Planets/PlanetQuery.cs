namespace GraphQL.Schemas.StarWars.Planets;

using GraphQL.Repositories.StarWars.Planets;

[ExtendObjectType("Query")]
public class PlanetQuery([Service] IPlanetRepository planets)
{
    [GraphQLDescription("A list of planets in Star Wars Universe.")]
    public async Task<IEnumerable<PlanetSchema>?> GetPlanetsAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await planets.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return null;
        }

        // Map and resolve
        return response.Results.Select(PlanetSchema.MapFrom);
    }

    [GraphQLDescription("A planet in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<PlanetSchema?> GetPlanetAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await planets.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return PlanetSchema.MapFrom(response);
    }
}
