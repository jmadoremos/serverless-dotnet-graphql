namespace GraphQL.Repositories.StarWars.Films;

using System.Globalization;
using GraphQL.Services.StarWars;

public class FilmRepository([Service] ISwapiService swapi) : IFilmRepository
{
    private readonly Uri baseUri = new("https://swapi.dev/api/films/");

    public async Task<SwapiResponseList<FilmApiResponse>> GetAllAsync(CancellationToken ctx)
    {
        // Define placeholder of results
        var result = new SwapiResponseList<FilmApiResponse>();

        // Define placeholder for the API response
        SwapiResponseList<FilmApiResponse>? response;

        // Define API URI to call. Defaults to resource URI.
        var uri = this.baseUri.ToString();

        do
        {
            // Call API
            response = await swapi
                .GetAsync<SwapiResponseList<FilmApiResponse>>(uri, ctx);

            // If the response does not have a data, return the result
            if (response == null || response.Count == 0)
            {
                break;
            }

            // Since the response has data, merge it to result
            foreach (var e in response.Results)
            {
                result.Results = result.Results.Append(e);
            }

            // Define the next URI to call based on the "next" property of the response
            result.Previous = uri;
            uri = response.Next;
        } while (uri is not null);

        result.Count = response?.Count ?? 0;
        result.Next = null;

        // Resolve
        return result;
    }

    public async Task<FilmApiResponse?> GetByIdAsync(
        int id,
        CancellationToken ctx)
    {
        // Define API URI to call
        var uri = new Uri(this.baseUri, Convert.ToString(id, CultureInfo.InvariantCulture));

        // Call API
        var result = await swapi
            .GetAsync<FilmApiResponse>(uri, ctx);

        // Resolve
        return result;
    }
}
