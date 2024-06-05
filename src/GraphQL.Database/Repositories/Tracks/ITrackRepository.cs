namespace GraphQL.Database.Repositories.Tracks;

using ErrorOr;
using GraphQL.Database.Repositories.Sessions;

public interface ITrackRepository
{
    Task<IQueryable<TrackModel>> GetAllTracksAsync(CancellationToken ctx);

    Task<TrackModel?> GetTrackByIdAsync(int id, CancellationToken ctx);

    Task<IQueryable<TrackModel>> GetTracksByIdsAsync(IEnumerable<int> ids, CancellationToken ctx);

    Task<TrackModel?> GetTrackByNameAsync(string name, CancellationToken ctx);

    Task<IQueryable<SessionModel>> GetSessionsAsync(int id, CancellationToken ctx);

    Task<ErrorOr<int>> CreateTrackAsync(TrackModelInput input, CancellationToken ctx);

    Task<ErrorOr<Updated>> UpdateTrackAsync(int id, TrackModelInput input, CancellationToken ctx);

    Task<ErrorOr<Deleted>> DeleteTrackAsync(int id, CancellationToken ctx);
}
