namespace GraphQL.Schemas.StarWars.Vehicles;

using GraphQL.Repositories.StarWars.Vehicles;

[ExtendObjectType("Query")]
public class VehicleQuery([Service] IVehicleRepository vehicles)
{
    public async Task<IEnumerable<VehicleSchema>?> GetStarshipsAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await vehicles.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return null;
        }

        // Map and resolve
        return response.Results.Select(VehicleSchema.MapFrom);
    }

    public async Task<VehicleSchema?> GetStarshipAsync(
        [GraphQLType(typeof(IdType))] int id,
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
        return VehicleSchema.MapFrom(response);
    }
}
