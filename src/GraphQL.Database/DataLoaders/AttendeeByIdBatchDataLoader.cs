namespace GraphQL.Database.DataLoaders;

using GraphQL.Database.Repositories.Attendees;
using GraphQL.Database.Schemas.Attendees;

public class AttendeeByIdBatchDataLoader(
    [Service] IAttendeeRepository attendees,
    IBatchScheduler batchScheduler,
    DataLoaderOptions? options = null)
    : BatchDataLoader<int, Attendee>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, Attendee>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var result = await attendees.GetAttendeesByIdsAsync(keys, cancellationToken);
        return result.Select(Attendee.MapFrom).ToDictionary(x => x.Id);
    }
}
