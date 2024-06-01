namespace GraphQL.Repositories.Database.SessionSpeakers;

using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.Speakers;

public interface ISessionSpeakerRepository
{
    Task<IQueryable<Speaker>> GetSpeakersBySessionAsync(int sessionId, CancellationToken ctx);

    Task<IQueryable<Session>> GetSessionsBySpeakerAsync(int speakerId, CancellationToken ctx);

    Task CreateSessionSpeakerAsync(SessionSpeakerInput input, CancellationToken ctx);

    Task DeleteSessionSpeakerAsync(SessionSpeakerInput input, CancellationToken ctx);
}
