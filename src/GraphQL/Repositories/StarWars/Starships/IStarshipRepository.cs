namespace GraphQL.Repositories.StarWars.Starships;

public interface IStarshipRepository
{
    Task<SwapiResponseList<StarshipApiResponse>> GetAllAsync(CancellationToken ctx);

    Task<StarshipApiResponse?> GetByIdAsync(int id, CancellationToken ctx);
}
