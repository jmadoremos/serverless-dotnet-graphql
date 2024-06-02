namespace GraphQL.DataLoaders.Database;

using GraphQL.Repositories.Database.Sessions;
using GraphQL.Schemas.Database.Sessions;

public class SessionByIdBatchDataLoader(
    [Service] ISessionRepository sessions,
    IBatchScheduler batchScheduler,
    DataLoaderOptions? options = null)
    : BatchDataLoader<int, Session>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, Session>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        // instead of fetching one person, we fetch multiple persons
        var result = await sessions.GetSessionsByIdsAsync(keys, cancellationToken);
        return result.Select(Session.MapFrom).ToDictionary(x => x.Id);
    }
}
