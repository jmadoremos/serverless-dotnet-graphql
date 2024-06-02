namespace GraphQL.WebAPI.Repositories.StarWars;

using GraphQL.WebAPI.Repositories.StarWars.Responses;

public interface IStarWarsRepository<TApiResponse>
    where TApiResponse : SwapiResponse
{
    Task<SwapiResponseList<TApiResponse>> GetAllAsync(CancellationToken ctx);

    Task<TApiResponse?> GetByIdAsync(int id, CancellationToken ctx);

    Task<SwapiResponseList<TApiResponse>> GetPageAsync(int page, CancellationToken ctx);
}
