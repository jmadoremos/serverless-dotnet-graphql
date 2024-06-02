namespace GraphQL.WebAPI.Repositories.StarWars;

using GraphQL.WebAPI.Repositories.StarWars.Responses;
using GraphQL.WebAPI.Services;
using GraphQL.WebAPI.Types;
using System.Collections.Concurrent;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

public class StarWarsRepository<TApiResponse>([Service] ISwapiService swapi)
    : IStarWarsRepository<TApiResponse>
    where TApiResponse : SwapiResponse
{
    public async Task<SwapiResponseList<TApiResponse>> GetAllAsync(
        CancellationToken ctx)
    {
        // Define placeholder of results
        var result = new SwapiResponseList<TApiResponse>();

        // Define placeholder of pages
        var pageResults = new ConcurrentDictionary<int, IEnumerable<TApiResponse>>();

        // Call first page to determine total count
        var page1 = await this.GetPageAsync(1, ctx);
        pageResults.TryAdd(1, page1.Results);

        // Calculate page size
        var pageTotalCount = page1.Count / page1.Results.Count();
        if (page1.Count % page1.Results.Count() > 0)
        {
            pageTotalCount += 1;
        }

        // Calculate remaining pages to parse
        var pageRemainingCount = pageTotalCount - 1;

        // Call the remaining pages in parallel
        await Parallel.ForEachAsync(
            Enumerable.Range(2, pageRemainingCount),
            new ParallelOptions
            {
                CancellationToken = ctx,
                MaxDegreeOfParallelism = 10
            },
            async (page, ct) =>
            {
                var pageN = await this.GetPageAsync(page, ct);
                pageResults.TryAdd(page, pageN.Results);
            });

        // Combine all results
        var pageCompleteResult = new List<TApiResponse>();
        foreach (var page in Enumerable.Range(1, pageTotalCount))
        {
            var pageResultOrNull = pageResults.GetValueOrDefault(page, default!);

            if (pageResultOrNull is not null)
            {
                pageCompleteResult.AddRange(pageResultOrNull);
            }
        }

        // Update the remaining metadata
        result.Count = page1.Count;
        result.Results = pageCompleteResult;
        result.Previous = $"{this.BaseUrl}?page={pageTotalCount}";
        result.Next = null;

        // Resolve
        return result;
    }

    public async Task<TApiResponse?> GetByIdAsync(
        int id,
        CancellationToken ctx)
    {
        // Define API URI to call
        var uri = new Uri(this.BaseUrl.BaseUri, Convert.ToString(id, CultureInfo.InvariantCulture));

        // Call API
        var result = await swapi
            .GetAsync<TApiResponse>(uri, ctx);

        // Resolve
        return result;
    }

    public async Task<SwapiResponseList<TApiResponse>> GetPageAsync(
        int page,
        CancellationToken ctx)
    {
        var uri = $"{this.BaseUrl}?page={page}";

        var response = await swapi.GetAsync<SwapiResponseList<TApiResponse>>(uri, ctx)
            ?? new SwapiResponseList<TApiResponse>();

        return response;
    }

    private SwapiUrl BaseUrl
    {
        get
        {
            if (typeof(TApiResponse) == typeof(CharacterApiResponse))
            {
                return SwapiUrl.CharactersUrl;
            }
            else if (typeof(TApiResponse) == typeof(FilmApiResponse))
            {
                return SwapiUrl.FilmsUrl;
            }
            else if (typeof(TApiResponse) == typeof(PlanetApiResponse))
            {
                return SwapiUrl.PlanetsUrl;
            }
            else if (typeof(TApiResponse) == typeof(SpeciesApiResponse))
            {
                return SwapiUrl.SpeciesUrl;
            }
            else if (typeof(TApiResponse) == typeof(StarshipApiResponse))
            {
                return SwapiUrl.StarshipsUrl;
            }
            else if (typeof(TApiResponse) == typeof(VehicleApiResponse))
            {
                return SwapiUrl.VehiclesUrl;
            }

            throw new ArgumentNullException(nameof(TApiResponse), "Type passed for TApiResponse is not supposed.");
        }
    }
}
