namespace GraphQL.Database.Repositories.SessionSpeakers;

using ErrorOr;
using GraphQL.Database;
using GraphQL.Database.Errors;
using GraphQL.Database.Repositories.Sessions;
using GraphQL.Database.Repositories.Speakers;
using Microsoft.EntityFrameworkCore;

public class SessionSpeakerRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : ISessionSpeakerRepository
{
    public async Task<IQueryable<SpeakerModel>> GetSpeakersBySessionAsync(
        int sessionId,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Sessions
            .Where(e => e.Id == sessionId)
            .Include(e => e.Speakers)
            .SelectMany(e => e.Speakers)
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<IQueryable<SessionModel>> GetSessionsBySpeakerAsync(
        int speakerId,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var result = await dbContext.Speakers
            .Where(e => e.Id == speakerId)
            .Include(e => e.Sessions)
            .SelectMany(e => e.Sessions)
            .ToListAsync(ctx);

        return result.AsQueryable();
    }

    public async Task<ErrorOr<Created>> CreateSessionSpeakerAsync(
        SessionSpeakerModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var session = dbContext.Sessions.AnyAsync(e => e.Id == input.SessionId, ctx);

        var speaker = dbContext.Speakers.AnyAsync(e => e.Id == input.SpeakerId, ctx);

        var mapping = dbContext.SessionSpeakers
            .AnyAsync(e =>
                e.SessionId == input.SessionId &&
                e.SpeakerId == input.SpeakerId,
                ctx);

        await Task.WhenAll(session, speaker, mapping);

        List<Error> errors = [];

        if (!session.Result)
        {
            errors.Add(SessionError.NotFound(input.SessionId));
        }

        if (!speaker.Result)
        {
            errors.Add(SpeakerError.NotFound(input.SpeakerId));
        }

        if (mapping.Result)
        {
            errors.Add(SessionSpeakerError.AlreadyExists(input.SessionId, input.SpeakerId));
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        var entity = SessionSpeakerModel.MapFrom(input);

        dbContext.SessionSpeakers
            .Add(entity);

        await dbContext.SaveChangesAsync(ctx);

        return Result.Created;
    }

    public async Task<ErrorOr<Deleted>> DeleteSessionSpeakerAsync(
        SessionSpeakerModelInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var session = dbContext.Sessions.AnyAsync(e => e.Id == input.SessionId, ctx);

        var speaker = dbContext.Speakers.AnyAsync(e => e.Id == input.SpeakerId, ctx);

        var mapping = dbContext.SessionSpeakers
            .AnyAsync(e =>
                e.SessionId == input.SessionId &&
                e.SpeakerId == input.SpeakerId,
                ctx);

        await Task.WhenAll(session, speaker, mapping);

        List<Error> errors = [];

        if (!session.Result)
        {
            errors.Add(SessionError.NotFound(input.SessionId));
        }

        if (!speaker.Result)
        {
            errors.Add(SpeakerError.NotFound(input.SpeakerId));
        }

        if (!mapping.Result)
        {
            errors.Add(SessionSpeakerError.NotFound(input.SessionId, input.SpeakerId));
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        var entity = SessionSpeakerModel.MapFrom(input);

        dbContext.SessionSpeakers
            .Remove(entity);

        await dbContext.SaveChangesAsync(ctx);

        return Result.Deleted;
    }
}
