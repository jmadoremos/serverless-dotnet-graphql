namespace GraphQL.Repositories.StarWars.Vehicles;

public interface IVehicleRepository
{
    Task<SwapiResponseList<VehicleApiResponse>> GetAllAsync(CancellationToken ctx);

    Task<VehicleApiResponse?> GetByIdAsync(int id, CancellationToken ctx);
}
