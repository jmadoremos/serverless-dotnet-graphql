namespace GraphQL.Database.Repositories.Sessions;

public interface ISessionRepository
{
    Task<IQueryable<SessionModel>> GetAllSessionsAsync(CancellationToken ctx);

    Task<SessionModel?> GetSessionByIdAsync(int id, CancellationToken ctx);

    Task<IQueryable<SessionModel>> GetSessionsByIdsAsync(IEnumerable<int> ids, CancellationToken ctx);

    Task<SessionModel?> GetSessionByTitleAsync(string title, CancellationToken ctx);

    Task<int> CreateSessionAsync(SessionModelInput input, CancellationToken ctx);

    Task UpdateSessionAsync(int id, SessionModelInput input, CancellationToken ctx);

    Task DeleteSessionAsync(int id, CancellationToken ctx);
}
