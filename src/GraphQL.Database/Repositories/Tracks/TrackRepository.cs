namespace GraphQL.Database.Repositories.Tracks;

using GraphQL.Database;
using GraphQL.Database.Exceptions;
using GraphQL.Database.Repositories.Sessions;
using Microsoft.EntityFrameworkCore;

public class TrackRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : ITrackRepository
{
    public async Task<IQueryable<TrackModel>> GetAllTracksAsync(CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Tracks
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<TrackModel?> GetTrackByIdAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Tracks
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<IQueryable<TrackModel>> GetTracksByIdsAsync(
        IEnumerable<int> ids,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Tracks
            .Where(e => ids.Contains(e.Id))
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<TrackModel?> GetTrackByNameAsync(
        string name,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Tracks
            .Where(e => e.Name == name)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<IQueryable<SessionModel>> GetSessionsAsync(
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

    public async Task<int> CreateTrackAsync(
        TrackModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.Tracks
            .AnyAsync(e => e.Name == input.Name, ctx);

        if (!existing)
        {
            throw new TrackNameTakenException();
        }

        var entity = TrackModel.MapFrom(input);

        dbContext.Tracks
            .Add(entity);

        await dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task UpdateTrackAsync(
        int id,
        TrackModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.Tracks
            .AnyAsync(e => e.Id == id, ctx);

        if (!existing)
        {
            throw new TrackNotFoundException();
        }

        dbContext.Tracks
            .Where(e => e.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(ee => ee.Name, input.Name));

        await dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteTrackAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.Tracks
            .AnyAsync(e => e.Id == id, ctx);

        if (!existing)
        {
            throw new TrackNotFoundException();
        }

        dbContext.Tracks
            .Where(e => e.Id == id)
            .ExecuteDelete();

        await dbContext.SaveChangesAsync(ctx);
    }
}
