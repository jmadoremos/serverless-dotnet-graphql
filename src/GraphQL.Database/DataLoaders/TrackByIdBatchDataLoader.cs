namespace GraphQL.Database.DataLoaders;

using GraphQL.Database.Repositories.Tracks;
using GraphQL.Database.Schemas.Tracks;

public class TrackByIdBatchDataLoader(
    [Service] ITrackRepository tracks,
    IBatchScheduler batchScheduler,
    DataLoaderOptions? options = null)
    : BatchDataLoader<int, Track>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, Track>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        // instead of fetching one person, we fetch multiple persons
        var result = await tracks.GetTracksByIdsAsync(keys, cancellationToken);
        return result.Select(Track.MapFrom).ToDictionary(x => x.Id);
    }
}
