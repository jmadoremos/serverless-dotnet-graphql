namespace GraphQL.Database.Repositories.SessionAttendees;

using ErrorOr;
using GraphQL.Database;
using GraphQL.Database.Errors;
using GraphQL.Database.Repositories.Attendees;
using GraphQL.Database.Repositories.Sessions;
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

        var result = await dbContext.Attendees
            .Where(e => e.Id == attendeeId)
            .Include(e => e.Sessions)
            .SelectMany(e => e.Sessions)
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<ErrorOr<Created>> CreateSessionAttendeeAsync(
        SessionAttendeeModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var session = dbContext.Sessions.AnyAsync(e => e.Id == input.SessionId, ctx);

        var attendee = dbContext.Attendees.AnyAsync(e => e.Id == input.AttendeeId, ctx);

        var mapping = dbContext.SessionAttendees
            .AnyAsync(e =>
                e.SessionId == input.SessionId &&
                e.AttendeeId == input.AttendeeId,
                ctx);

        await Task.WhenAll(session, attendee, mapping);

        List<Error> errors = [];

        if (!session.Result)
        {
            errors.Add(SessionError.NotFound(input.SessionId));
        }

        if (!attendee.Result)
        {
            errors.Add(AttendeeError.NotFound(input.AttendeeId));
        }

        if (mapping.Result)
        {
            errors.Add(SessionAttendeeError.AlreadyExists(input.SessionId, input.AttendeeId));
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        var entity = SessionAttendeeModel.MapFrom(input);

        dbContext.SessionAttendees.Add(entity);

        await dbContext.SaveChangesAsync(ctx);

        return Result.Created;
    }

    public async Task<ErrorOr<Deleted>> DeleteSessionAttendeeAsync(
        SessionAttendeeModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var session = dbContext.Sessions.AnyAsync(e => e.Id == input.SessionId, ctx);

        var attendee = dbContext.Attendees.AnyAsync(e => e.Id == input.AttendeeId, ctx);

        var mapping = dbContext.SessionAttendees
            .AnyAsync(e =>
                e.SessionId == input.SessionId &&
                e.AttendeeId == input.AttendeeId,
                ctx);

        await Task.WhenAll(session, attendee, mapping);

        List<Error> errors = [];

        if (!session.Result)
        {
            errors.Add(SessionError.NotFound(input.SessionId));
        }

        if (!attendee.Result)
        {
            errors.Add(AttendeeError.NotFound(input.AttendeeId));
        }

        if (!mapping.Result)
        {
            errors.Add(SessionAttendeeError.NotFound(input.SessionId, input.AttendeeId));
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        var entity = SessionAttendeeModel.MapFrom(input);

        dbContext.SessionAttendees
            .Remove(entity);

        await dbContext.SaveChangesAsync(ctx);

        return Result.Deleted;
    }
}
