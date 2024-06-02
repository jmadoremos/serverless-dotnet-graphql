namespace GraphQL.Repositories.Database.SessionAttendees;

using GraphQL.Data;
using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Attendees;
using GraphQL.Repositories.Database.Sessions;
using Microsoft.EntityFrameworkCore;

public class SessionAttendeeRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : ISessionAttendeeRepository
{
    public async Task<IQueryable<AttendeeModel>> GetAttendeesBySessionAsync(
        int sessionId,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Sessions
            .Where(e => e.Id == sessionId)
            .Include(e => e.Attendees)
            .SelectMany(e => e.Attendees)
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<IQueryable<SessionModel>> GetSessionsByAttendeeAsync(
        int attendeeId,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Speakers
            .Where(e => e.Id == attendeeId)
            .Include(e => e.Sessions)
            .SelectMany(e => e.Sessions)
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task CreateSessionAttendeeAsync(
        SessionAttendeeModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.SessionAttendees
            .AnyAsync(e =>
                e.SessionId == input.SessionId &&
                e.AttendeeId == input.AttendeeId,
                ctx);

        if (existing)
        {
            throw new SessionSpeakerExistsException();
        }

        var entity = SessionAttendeeModel.MapFrom(input);

        dbContext.SessionAttendees.Add(entity);

        await dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteSessionAttendeeAsync(
        SessionAttendeeModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.SessionAttendees
            .AnyAsync(e =>
                e.SessionId == input.SessionId &&
                e.AttendeeId == input.AttendeeId,
                ctx);

        if (!existing)
        {
            throw new SessionSpeakerNotFoundException();
        }

        var entity = SessionAttendeeModel.MapFrom(input);

        dbContext.SessionAttendees
            .Remove(entity);

        await dbContext.SaveChangesAsync(ctx);
    }
}
