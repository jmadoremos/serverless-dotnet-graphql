namespace GraphQL.Database.Repositories.Speakers;

using ErrorOr;
using GraphQL.Database;
using GraphQL.Database.Errors;
using Microsoft.EntityFrameworkCore;

public class SpeakerRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : ISpeakerRepository
{
    public async Task<IQueryable<SpeakerModel>> GetAllSpeakersAsync(CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Speakers
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<SpeakerModel?> GetSpeakerByIdAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Speakers
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<IQueryable<SpeakerModel>> GetSpeakersByIdsAsync(
        IEnumerable<int> ids,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Speakers
            .Where(e => ids.Contains(e.Id))
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<SpeakerModel?> GetSpeakerByNameAsync(
        string name,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Speakers
            .Where(e => e.Name == name)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<ErrorOr<int>> CreateSpeakerAsync(
        SpeakerModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.Speakers
            .AnyAsync(e => e.Name == input.Name, ctx);

        if (!existing)
        {
            return SpeakerError.NameTaken(input.Name);
        }

        var entity = SpeakerModel.MapFrom(input);

        dbContext.Speakers
            .Add(entity);

        await dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task<ErrorOr<Updated>> UpdateSpeakerAsync(
        int id,
        SpeakerModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.Speakers
            .AnyAsync(e => e.Id == id, ctx);

        if (!existing)
        {
            return SpeakerError.NotFound(id);
        }

        dbContext.Speakers
            .Where(e => e.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(ee => ee.Name, input.Name)
                .SetProperty(ee => ee.Bio, input.Bio)
                .SetProperty(ee => ee.WebSite, input.WebSite));

        await dbContext.SaveChangesAsync(ctx);

        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteSpeakerAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.Speakers
            .AnyAsync(e => e.Id == id, ctx);

        if (!existing)
        {
            return SpeakerError.NotFound(id);
        }

        dbContext.Speakers
            .Where(e => e.Id == id)
            .ExecuteDelete();

        await dbContext.SaveChangesAsync(ctx);

        return Result.Deleted;
    }
}
