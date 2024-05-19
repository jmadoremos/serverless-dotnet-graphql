namespace GraphQL.Services.StarWars;

using Newtonsoft.Json;

public class SwapiService(HttpClient client) : ISwapiService
{
    private readonly HttpClient client = client;

    public async Task<T?> GetAsync<T>(
        string url,
        CancellationToken ctx)
    {
        var response = await this.client.GetAsync(url, ctx);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(ctx);
        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task<T?> GetAsync<T>(
        Uri url,
        CancellationToken ctx)
    {
        var response = await this.client.GetAsync(url, ctx);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(ctx);
        return JsonConvert.DeserializeObject<T>(content);
    }
}
