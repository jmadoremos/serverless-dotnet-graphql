namespace GraphQL.Repositories.Database.Tracks;

using GraphQL.Repositories.Database.Sessions;

public interface ITrackRepository
{
    Task<IQueryable<TrackModel>> GetAllTracksAsync(CancellationToken ctx);

    Task<TrackModel?> GetTrackByIdAsync(int id, CancellationToken ctx);

    Task<IQueryable<TrackModel>> GetTracksByIdsAsync(IEnumerable<int> ids, CancellationToken ctx);

    Task<TrackModel?> GetTrackByNameAsync(string name, CancellationToken ctx);

    Task<IQueryable<SessionModel>> GetSessionsAsync(int id, CancellationToken ctx);

    Task<int> CreateTrackAsync(TrackModelInput input, CancellationToken ctx);

    Task UpdateTrackAsync(int id, TrackModelInput input, CancellationToken ctx);

    Task DeleteTrackAsync(int id, CancellationToken ctx);
}
