namespace GraphQL.Repositories.StarWars.Species;

using GraphQL.Services.StarWars;
using System.Collections.Concurrent;
using System.Globalization;

public class SpeciesRepository([Service] ISwapiService swapi) : ISpeciesRepository
{
    private readonly Uri baseUri = new("https://swapi.dev/api/species/");

    public async Task<SwapiResponseList<SpeciesApiResponse>> GetAllAsync(CancellationToken ctx)
    {
        // Define placeholder of results
        var result = new SwapiResponseList<SpeciesApiResponse>();

        // Define placeholder of pages
        var pageResults = new ConcurrentDictionary<int, IEnumerable<SpeciesApiResponse>>();

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
        var pageCompleteResult = new List<SpeciesApiResponse>();
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
        result.Previous = $"{this.baseUri}?page={pageTotalCount}";
        result.Next = null;

        // Resolve
        return result;
    }

    public async Task<SpeciesApiResponse?> GetByIdAsync(
        int id,
        CancellationToken ctx)
    {
        // Define API URI to call
        var uri = new Uri(this.baseUri, Convert.ToString(id, CultureInfo.InvariantCulture));

        // Call API
        var result = await swapi
            .GetAsync<SpeciesApiResponse>(uri, ctx);

        // Resolve
        return result;
    }

    private async Task<SwapiResponseList<SpeciesApiResponse>> GetPageAsync(
        int page,
        CancellationToken ctx)
    {
        var uri = $"{this.baseUri}?page={page}";

        var response = await swapi.GetAsync<SwapiResponseList<SpeciesApiResponse>>(uri, ctx)
            ?? new SwapiResponseList<SpeciesApiResponse>();

        return response;
    }
}
