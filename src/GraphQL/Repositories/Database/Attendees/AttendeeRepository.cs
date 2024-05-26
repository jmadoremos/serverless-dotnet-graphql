namespace GraphQL.Repositories.Database.Attendees;

using GraphQL.Data;
using GraphQL.Exceptions.Database;
using Microsoft.EntityFrameworkCore;

public class AttendeeRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : IAttendeeRepository
{
    private readonly ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();

    public async Task<IQueryable<Attendee>> GetAllAsync(CancellationToken ctx)
    {
        var result = await this.dbContext.Attendees.ToListAsync(ctx);
        return result.AsQueryable();
    }

    public async Task<Attendee> GetByIdAsync(
        int id,
        CancellationToken ctx) =>
        await this.dbContext.Attendees
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(ctx)
            ?? throw new UserNotFoundException(nameof(Attendee.Id));

    public async Task<Attendee> GetByUserNameAsync(
        string userName,
        CancellationToken ctx) =>
        await this.dbContext.Attendees
            .Where(w => w.UserName == userName)
            .FirstOrDefaultAsync(ctx)
            ?? throw new UserNotFoundException(nameof(Attendee.UserName));

    public async Task<int> CreateAsync(
        AttendeeInput input,
        CancellationToken ctx)
    {
        var existing = await this.GetByUserNameAsync(input.UserName, ctx);

        if (existing is not null)
        {
            throw new UsernameTakenException();
        }

        var entity = Attendee.MapFrom(input);
        this.dbContext.Attendees.Add(entity);

        await this.dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task UpdateAsync(
        int id,
        AttendeeInput input,
        CancellationToken ctx)
    {
        var _ = await this.GetByIdAsync(id, ctx);

        this.dbContext.Attendees
            .Where(w => w.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(s => s.FirstName, input.FirstName)
                .SetProperty(s => s.LastName, input.LastName)
                .SetProperty(s => s.EmailAddress, input.EmailAddress));

        await this.dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken ctx)
    {
        var _ = await this.GetByIdAsync(id, ctx);

        this.dbContext.Attendees
            .Where(w => w.Id == id)
            .ExecuteDelete();

        await this.dbContext.SaveChangesAsync(ctx);
    }
}
