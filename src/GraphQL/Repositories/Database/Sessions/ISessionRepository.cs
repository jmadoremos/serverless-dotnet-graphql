namespace GraphQL.Repositories.Database.Sessions;

public interface ISessionRepository
{
    Task<IQueryable<Session>> GetAllAsync(CancellationToken ctx);

    Task<Session?> GetByIdAsync(int id, CancellationToken ctx);

    Task<Session?> GetByTitleAsync(string title, CancellationToken ctx);

    Task<int> CreateAsync(SessionInput input, CancellationToken ctx);

    Task UpdateAsync(int id, SessionInput input, CancellationToken ctx);

    Task DeleteAsync(int id, CancellationToken ctx);
}
