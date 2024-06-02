namespace GraphQL.StarWars.Schemas.Planets;

using GraphQL.StarWars.Repositories;
using GraphQL.StarWars.Repositories.Responses;

[ExtendObjectType("Query")]
public class PlanetQuery(
    [Service] IStarWarsRepository<PlanetApiResponse> planets)
{
    [UsePaging]
    [GraphQLDescription("A list of planets in Star Wars Universe.")]
    public async Task<IEnumerable<Planet>> GetPlanetsAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await planets.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map and resolve
        return response.Results.Select(Planet.MapFrom);
    }

    [GraphQLDescription("A planet in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Planet?> GetPlanetByIdAsync(
        [ID(nameof(Planet))] int id,
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
        return Planet.MapFrom(response);
    }
}
