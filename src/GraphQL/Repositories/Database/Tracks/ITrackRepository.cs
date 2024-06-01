namespace GraphQL.Repositories.Database.Tracks;

using GraphQL.Repositories.Database.Sessions;

public interface ITrackRepository
{
    Task<IQueryable<Track>> GetAllAsync(CancellationToken ctx);

    Task<Track?> GetByIdAsync(int id, CancellationToken ctx);

    Task<Track?> GetByNameAsync(string name, CancellationToken ctx);

    Task<IQueryable<Session>> GetSessionsAsync(int id, CancellationToken ctx);

    Task<int> CreateAsync(TrackInput input, CancellationToken ctx);

    Task UpdateAsync(int id, TrackInput input, CancellationToken ctx);

    Task DeleteAsync(int id, CancellationToken ctx);
}
