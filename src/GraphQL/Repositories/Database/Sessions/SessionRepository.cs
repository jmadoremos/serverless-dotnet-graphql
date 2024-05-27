namespace GraphQL.Repositories.Database.Sessions;

using GraphQL.Data;
using GraphQL.Exceptions.Database;
using Microsoft.EntityFrameworkCore;

public class SessionRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : ISessionRepository
{
    private readonly ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();

    public async Task<IQueryable<Session>> GetAllAsync(CancellationToken ctx)
    {
        var result = await this.dbContext.Sessions.ToListAsync(ctx);
        return result.AsQueryable();
    }

    public async Task<Session?> GetByIdAsync(
        int id,
        CancellationToken ctx) =>
        await this.dbContext.Sessions
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(ctx);

    public async Task<Session?> GetByTitleAsync(
        string title,
        CancellationToken ctx) =>
        await this.dbContext.Sessions
            .Where(w => w.Title == title)
            .FirstOrDefaultAsync(ctx);

    public async Task<int> CreateAsync(
        SessionInput input,
        CancellationToken ctx)
    {
        var existing = await this.GetByTitleAsync(input.Title, ctx);

        if (existing is not null)
        {
            throw new SessionTitleTakenException();
        }

        var entity = Session.MapFrom(input);
        this.dbContext.Sessions.Add(entity);

        await this.dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task UpdateAsync(
        int id,
        SessionInput input,
        CancellationToken ctx)
    {
        var _ = await this.GetByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        this.dbContext.Sessions
            .Where(w => w.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(s => s.Title, input.Title)
                .SetProperty(s => s.Abstract, input.Abstract)
                .SetProperty(s => s.StartTime, input.StartTime)
                .SetProperty(s => s.EndTime, input.EndTime)
                .SetProperty(s => s.TrackId, input.TrackId));

        await this.dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken ctx)
    {
        var _ = await this.GetByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        this.dbContext.Sessions
            .Where(w => w.Id == id)
            .ExecuteDelete();

        await this.dbContext.SaveChangesAsync(ctx);
    }
}
