namespace GraphQL.Repositories.Database.SessionSpeakers;

using GraphQL.Data;
using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.Speakers;
using Microsoft.EntityFrameworkCore;

public class SessionSpeakerRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory) : ISessionSpeakerRepository
{
    public async Task<IQueryable<Speaker>> GetSpeakersBySessionAsync(
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

    public async Task<IQueryable<Session>> GetSessionsBySpeakerAsync(
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

    public async Task CreateSessionSpeakerAsync(
        SessionSpeakerInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.SessionSpeakers
            .AnyAsync(e =>
                e.SessionId == input.SessionId &&
                e.SpeakerId == input.SpeakerId,
                ctx);

        if (existing)
        {
            throw new SessionSpeakerExistsException();
        }

        var entity = SessionSpeaker.MapFrom(input);

        dbContext.SessionSpeakers.Add(entity);
        await dbContext.SaveChangesAsync(ctx);
    }

    public async Task DeleteSessionSpeakerAsync(
        SessionSpeakerInput input,
        CancellationToken ctx)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(ctx);

        var existing = await dbContext.SessionSpeakers
            .AnyAsync(e =>
                e.SessionId == input.SessionId &&
                e.SpeakerId == input.SpeakerId,
                ctx);

        if (!existing)
        {
            throw new SessionSpeakerNotFoundException();
        }

        var entity = SessionSpeaker.MapFrom(input);

        dbContext.SessionSpeakers.Remove(entity);
        await dbContext.SaveChangesAsync(ctx);
    }
}
