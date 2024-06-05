namespace GraphQL.Database.Repositories.Sessions;

using ErrorOr;

public interface ISessionRepository
{
    Task<IQueryable<SessionModel>> GetAllSessionsAsync(CancellationToken ctx);

    Task<SessionModel?> GetSessionByIdAsync(int id, CancellationToken ctx);

    Task<IQueryable<SessionModel>> GetSessionsByIdsAsync(IEnumerable<int> ids, CancellationToken ctx);

    Task<SessionModel?> GetSessionByTitleAsync(string title, CancellationToken ctx);

    Task<ErrorOr<int>> CreateSessionAsync(SessionModelInput input, CancellationToken ctx);

    Task<ErrorOr<Updated>> UpdateSessionAsync(int id, SessionModelInput input, CancellationToken ctx);

    Task<ErrorOr<Deleted>> DeleteSessionAsync(int id, CancellationToken ctx);
}
