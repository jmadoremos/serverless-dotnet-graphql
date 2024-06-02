namespace GraphQL.Repositories.Database.SessionSpeakers;

using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.Speakers;

public interface ISessionSpeakerRepository
{
    Task<IQueryable<SpeakerModel>> GetSpeakersBySessionAsync(int sessionId, CancellationToken ctx);

    Task<IQueryable<SessionModel>> GetSessionsBySpeakerAsync(int speakerId, CancellationToken ctx);

    Task CreateSessionSpeakerAsync(SessionSpeakerModelInput input, CancellationToken ctx);

    Task DeleteSessionSpeakerAsync(SessionSpeakerModelInput input, CancellationToken ctx);
}
