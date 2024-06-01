namespace GraphQL.Repositories.Database.Speakers;

using GraphQL.Data;
using GraphQL.Exceptions.Database;
using Microsoft.EntityFrameworkCore;

public class SpeakerRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : ISpeakerRepository
{
    public async Task<IQueryable<Speaker>> GetAllAsync(CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);
        var result = await dbContext.Speakers.ToListAsync(ctx);
        return result.AsQueryable();
    }

    public async Task<Speaker?> GetByIdAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Speakers
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<Speaker?> GetByNameAsync(
        string name,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Speakers
            .Where(w => w.Name == name)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<int> CreateAsync(
        SpeakerInput input,
        CancellationToken ctx)
    {
        var existing = await this.GetByNameAsync(input.Name, ctx);

        if (existing is not null)
        {
            throw new UsernameTakenException(nameof(Speaker.Name));
        }

        var entity = Speaker.MapFrom(input);

        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        dbContext.Speakers.Add(entity);
        await dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task UpdateAsync(
        int id,
        SpeakerInput input,
        CancellationToken ctx)
    {
        var _ = await this.GetByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(Speaker.Id));

        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        dbContext.Speakers
            .Where(w => w.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(s => s.Name, input.Name)
                .SetProperty(s => s.Bio, input.Bio)
                .SetProperty(s => s.WebSite, input.WebSite));

        await dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken ctx)
    {
        var _ = await this.GetByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(Speaker.Id));

        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        dbContext.Speakers
            .Where(w => w.Id == id)
            .ExecuteDelete();

        await dbContext.SaveChangesAsync(ctx);
    }
}
