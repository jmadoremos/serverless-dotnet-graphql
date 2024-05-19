namespace GraphQL.Repositories.StarWars.Planets;

public interface IPlanetRepository
{
    Task<SwapiResponseList<PlanetApiResponse>> GetAllAsync(CancellationToken ctx);

    Task<PlanetApiResponse?> GetByIdAsync(int id, CancellationToken ctx);
}
