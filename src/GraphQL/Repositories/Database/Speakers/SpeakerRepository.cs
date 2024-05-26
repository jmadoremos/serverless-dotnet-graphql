namespace GraphQL.Repositories.Database.Speakers;

using GraphQL.Data;
using GraphQL.Exceptions.Database;
using Microsoft.EntityFrameworkCore;

public class SpeakerRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : ISpeakerRepository
{
    private readonly ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();

    public async Task<IQueryable<Speaker>> GetAllAsync(CancellationToken ctx)
    {
        var result = await this.dbContext.Speakers.ToListAsync(ctx);
        return result.AsQueryable();
    }

    public async Task<Speaker> GetByIdAsync(
        int id,
        CancellationToken ctx) =>
        await this.dbContext.Speakers
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(ctx)
            ?? throw new UserNotFoundException(nameof(Speaker.Id));

    public async Task<int> CreateAsync(
        SpeakerInput input,
        CancellationToken ctx)
    {
        var entity = Speaker.MapFrom(input);
        this.dbContext.Speakers.Add(entity);

        await this.dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task UpdateAsync(
        int id,
        SpeakerInput input,
        CancellationToken ctx)
    {
        var _ = await this.GetByIdAsync(id, ctx);

        this.dbContext.Speakers
            .Where(w => w.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(s => s.Name, input.Name)
                .SetProperty(s => s.Bio, input.Bio)
                .SetProperty(s => s.WebSite, input.WebSite));

        await this.dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken ctx)
    {
        var _ = await this.GetByIdAsync(id, ctx);

        this.dbContext.Speakers
            .Where(w => w.Id == id)
            .ExecuteDelete();

        await this.dbContext.SaveChangesAsync(ctx);
    }
}
