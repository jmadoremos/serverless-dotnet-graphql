namespace GraphQL.Database.Repositories.Attendees;

using ErrorOr;
using GraphQL.Database;
using GraphQL.Database.Errors;
using Microsoft.EntityFrameworkCore;

public class AttendeeRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : IAttendeeRepository
{
    public async Task<IQueryable<AttendeeModel>> GetAllAttendeesAsync(CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Attendees
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<AttendeeModel?> GetAttendeeByIdAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Attendees
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<IQueryable<AttendeeModel>> GetAttendeesByIdsAsync(
        IEnumerable<int> ids,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Attendees
            .Where(e => ids.Contains(e.Id))
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<AttendeeModel?> GetAttendeeByUserNameAsync(
        string userName,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Attendees
            .Where(e => e.UserName == userName)
            .FirstOrDefaultAsync(ctx);

        return result;
    }

    public async Task<ErrorOr<int>> CreateAttendeeAsync(
        AttendeeModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.Attendees
            .AnyAsync(e => e.UserName == input.UserName, ctx);

        if (!existing)
        {
            return AttendeeError.UserNameTaken(input.UserName);
        }

        var entity = AttendeeModel.MapFrom(input);

        dbContext.Attendees
            .Add(entity);

        await dbContext.SaveChangesAsync(ctx);

        return entity.Id;
    }

    public async Task<ErrorOr<Updated>> UpdateAttendeeAsync(
        int id,
        AttendeeModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.Attendees
            .AnyAsync(e => e.Id == id, ctx);

        if (!existing)
        {
            return AttendeeError.NotFound(id);
        }

        dbContext.Attendees
            .Where(w => w.Id == id)
            .ExecuteUpdate(e => e
                .SetProperty(s => s.FirstName, input.FirstName)
                .SetProperty(s => s.LastName, input.LastName)
                .SetProperty(s => s.EmailAddress, input.EmailAddress));

        await dbContext.SaveChangesAsync(ctx);

        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteAttendeeAsync(
        int id,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.Attendees
            .AnyAsync(e => e.Id == id, ctx);

        if (!existing)
        {
            return AttendeeError.NotFound(id);
        }

        dbContext.Attendees
            .Where(w => w.Id == id)
            .ExecuteDelete();

        await dbContext.SaveChangesAsync(ctx);

        return Result.Deleted;
    }
}
