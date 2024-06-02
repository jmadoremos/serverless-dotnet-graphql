namespace GraphQL.Database.Repositories.SessionSpeakers;

using GraphQL.Database.Repositories.Sessions;
using GraphQL.Database.Repositories.Speakers;

public interface ISessionSpeakerRepository
{
    Task<IQueryable<SpeakerModel>> GetSpeakersBySessionAsync(int sessionId, CancellationToken ctx);

    Task<IQueryable<SessionModel>> GetSessionsBySpeakerAsync(int speakerId, CancellationToken ctx);

    Task CreateSessionSpeakerAsync(SessionSpeakerModelInput input, CancellationToken ctx);

    Task DeleteSessionSpeakerAsync(SessionSpeakerModelInput input, CancellationToken ctx);
}
