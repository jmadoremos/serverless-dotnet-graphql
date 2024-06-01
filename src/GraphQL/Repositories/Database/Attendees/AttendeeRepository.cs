namespace GraphQL.Repositories.Database.Attendees;

using GraphQL.Data;
using GraphQL.Exceptions.Database;
using Microsoft.EntityFrameworkCore;

public class AttendeeRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : IAttendeeRepository
{
    public async Task<IQueryable<Attendee>> GetAllAsync(CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);
        var result = await dbContext.Attendees.ToListAsync(ctx);
        return result.AsQueryable();
    }

    public async Task<Attendee?> GetByIdAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);
        var result = await dbContext.Attendees
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(ctx);
        return result;
    }

    public async Task<Attendee?> GetByUserNameAsync(
        string userName,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);
        var result = await dbContext.Attendees
            .Where(e => e.UserName == userName)
            .FirstOrDefaultAsync(ctx);
        return result;
    }

    public async Task<int> CreateAsync(
        AttendeeInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await this.GetByUserNameAsync(input.UserName, ctx);

        if (existing is not null)
        {
            throw new UsernameTakenException(nameof(Attendee.UserName));
        }

        var entity = Attendee.MapFrom(input);

        dbContext.Attendees.Add(entity);

        await dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task UpdateAsync(
        int id,
        AttendeeInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);
        var _ = await this.GetByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(Attendee.Id));

        dbContext.Attendees
            .Where(w => w.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(s => s.FirstName, input.FirstName)
                .SetProperty(s => s.LastName, input.LastName)
                .SetProperty(s => s.EmailAddress, input.EmailAddress));

        await dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);
        var _ = await this.GetByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(Attendee.Id));

        dbContext.Attendees
            .Where(w => w.Id == id)
            .ExecuteDelete();

        await dbContext.SaveChangesAsync(ctx);
    }
}
