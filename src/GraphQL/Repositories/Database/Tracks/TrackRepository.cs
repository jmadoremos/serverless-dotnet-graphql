namespace GraphQL.Repositories.Database.Tracks;

using GraphQL.Data;
using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Sessions;
using Microsoft.EntityFrameworkCore;

public class TrackRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : ITrackRepository
{
    public async Task<IQueryable<Track>> GetAllAsync(CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Tracks
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<Track?> GetByIdAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Tracks
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<Track?> GetByNameAsync(
        string name,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Tracks
            .Where(w => w.Name == name)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<IQueryable<Session>> GetSessionsAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Tracks
            .Where(e => e.Id == id)
            .Include(e => e.Sessions)
            .SelectMany(e => e.Sessions)
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<int> CreateAsync(
        TrackInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await this.GetByNameAsync(input.Name, ctx);

        if (existing is not null)
        {
            throw new TrackNameTakenException();
        }

        var entity = Track.MapFrom(input);
        dbContext.Tracks.Add(entity);

        await dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task UpdateAsync(
        int id,
        TrackInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var _ = await this.GetByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        dbContext.Tracks
            .Where(w => w.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(s => s.Name, input.Name));

        await dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var _ = await this.GetByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        dbContext.Tracks
            .Where(w => w.Id == id)
            .ExecuteDelete();

        await dbContext.SaveChangesAsync(ctx);
    }
}
