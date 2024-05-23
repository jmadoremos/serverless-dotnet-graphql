namespace GraphQL.Repositories.StarWars.Species;

public interface ISpeciesRepository
{
    Task<SwapiResponseList<SpeciesApiResponse>> GetAllAsync(CancellationToken ctx);

    Task<SpeciesApiResponse?> GetByIdAsync(int id, CancellationToken ctx);
}
