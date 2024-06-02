namespace GraphQL.StarWars.Schemas.Vehicles;

using GraphQL.StarWars.Repositories;
using GraphQL.StarWars.Repositories.Responses;

[ExtendObjectType("Query")]
public class VehicleQuery(
    [Service] IStarWarsRepository<VehicleApiResponse> vehicles)
{
    [UsePaging]
    [GraphQLDescription("A list of vehicle in Star Wars Universe.")]
    public async Task<IEnumerable<Vehicle>> GetVehiclesAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await vehicles.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map and resolve
        return response.Results.Select(Vehicle.MapFrom);
    }

    [GraphQLDescription("A vehicle in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Vehicle?> GetVehicleByIdAsync(
        [ID(nameof(Vehicle))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await vehicles.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return Vehicle.MapFrom(response);
    }
}
