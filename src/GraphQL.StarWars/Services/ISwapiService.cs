namespace GraphQL.StarWars.Services;

public interface ISwapiService
{
    Task<T?> GetAsync<T>(string url, CancellationToken ctx);
    Task<T?> GetAsync<T>(Uri url, CancellationToken ctx);
}
