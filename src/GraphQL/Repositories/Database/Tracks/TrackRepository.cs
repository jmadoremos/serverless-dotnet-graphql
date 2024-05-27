namespace GraphQL.Repositories.Database.Tracks;

using GraphQL.Data;
using GraphQL.Exceptions.Database;
using Microsoft.EntityFrameworkCore;

public class TrackRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : ITrackRepository
{
    private readonly ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();

    public async Task<IQueryable<Track>> GetAllAsync(CancellationToken ctx)
    {
        var result = await this.dbContext.Tracks.ToListAsync(ctx);
        return result.AsQueryable();
    }

    public async Task<Track?> GetByIdAsync(
        int id,
        CancellationToken ctx) =>
        await this.dbContext.Tracks
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(ctx);

    public async Task<Track?> GetByNameAsync(
        string name,
        CancellationToken ctx) =>
        await this.dbContext.Tracks
            .Where(w => w.Name == name)
            .FirstOrDefaultAsync(ctx);

    public async Task<int> CreateAsync(
        TrackInput input,
        CancellationToken ctx)
    {
        var existing = await this.GetByNameAsync(input.Name, ctx);

        if (existing is not null)
        {
            throw new TrackNameTakenException();
        }

        var entity = Track.MapFrom(input);
        this.dbContext.Tracks.Add(entity);

        await this.dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task UpdateAsync(
        int id,
        TrackInput input,
        CancellationToken ctx)
    {
        var _ = await this.GetByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        this.dbContext.Tracks
            .Where(w => w.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(s => s.Name, input.Name));

        await this.dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken ctx)
    {
        var _ = await this.GetByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        this.dbContext.Tracks
            .Where(w => w.Id == id)
            .ExecuteDelete();

        await this.dbContext.SaveChangesAsync(ctx);
    }
}
