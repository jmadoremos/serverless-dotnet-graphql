namespace GraphQL.Database.Repositories.Sessions;

using GraphQL.Database;
using GraphQL.Database.Exceptions;
using Microsoft.EntityFrameworkCore;

public class SessionRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : ISessionRepository
{
    public async Task<IQueryable<SessionModel>> GetAllSessionsAsync(CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Sessions
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<SessionModel?> GetSessionByIdAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Sessions
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<IQueryable<SessionModel>> GetSessionsByIdsAsync(
        IEnumerable<int> ids,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Sessions
            .Where(e => ids.Contains(e.Id))
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<SessionModel?> GetSessionByTitleAsync(
        string title,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Sessions
            .Where(e => e.Title == title)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<int> CreateSessionAsync(
        SessionModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await this.GetSessionByTitleAsync(input.Title, ctx);

        if (existing is not null)
        {
            throw new SessionTitleTakenException();
        }

        var entity = SessionModel.MapFrom(input);

        dbContext.Sessions
            .Add(entity);

        await dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task UpdateSessionAsync(
        int id,
        SessionModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var _ = await this.GetSessionByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        dbContext.Sessions
            .Where(w => w.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(ee => ee.Title, input.Title)
                .SetProperty(ee => ee.Abstract, input.Abstract)
                .SetProperty(ee => ee.StartTime, input.StartTime)
                .SetProperty(ee => ee.EndTime, input.EndTime)
                .SetProperty(ee => ee.TrackId, input.TrackId));

        await dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteSessionAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var _ = await this.GetSessionByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        dbContext.Sessions
            .Where(e => e.Id == id)
            .ExecuteDelete();

        await dbContext.SaveChangesAsync(ctx);
    }
}
