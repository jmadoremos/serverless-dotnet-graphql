namespace GraphQL.DataLoaders.Database;

using GraphQL.Repositories.Database.Speakers;
using GraphQL.Schemas.Database.Speakers;

public class SpeakerByIdBatchDataLoader(
    [Service] ISpeakerRepository speakers,
    IBatchScheduler batchScheduler,
    DataLoaderOptions? options = null)
    : BatchDataLoader<int, Speaker>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, Speaker>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        // instead of fetching one person, we fetch multiple persons
        var result = await speakers.GetSpeakersByIdsAsync(keys, cancellationToken);
        return result.Select(Speaker.MapFrom).ToDictionary(x => x.Id);
    }
}
