namespace GraphQL.Services.StarWars;

public interface ISwapiService
{
    Task<T?> GetAsync<T>(string url, CancellationToken ctx);
    Task<T?> GetAsync<T>(Uri url, CancellationToken ctx);
}
