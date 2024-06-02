namespace GraphQL.Repositories.Database.Speakers;

using GraphQL.Data;
using GraphQL.Exceptions.Database;
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

    public async Task<int> CreateSpeakerAsync(
        SpeakerModelInput input,
        CancellationToken ctx)
    {
        var existing = await this.GetSpeakerByNameAsync(input.Name, ctx);

        if (existing is not null)
        {
            throw new UsernameTakenException(nameof(SpeakerModel.Name));
        }

        var entity = SpeakerModel.MapFrom(input);

        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        dbContext.Speakers
            .Add(entity);

        await dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task UpdateSpeakerAsync(
        int id,
        SpeakerModelInput input,
        CancellationToken ctx)
    {
        var _ = await this.GetSpeakerByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(SpeakerModel.Id));

        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        dbContext.Speakers
            .Where(e => e.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(ee => ee.Name, input.Name)
                .SetProperty(ee => ee.Bio, input.Bio)
                .SetProperty(ee => ee.WebSite, input.WebSite));

        await dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteSpeakerAsync(
        int id,
        CancellationToken ctx)
    {
        var _ = await this.GetSpeakerByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(SpeakerModel.Id));

        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        dbContext.Speakers
            .Where(e => e.Id == id)
            .ExecuteDelete();

        await dbContext.SaveChangesAsync(ctx);
    }
}
