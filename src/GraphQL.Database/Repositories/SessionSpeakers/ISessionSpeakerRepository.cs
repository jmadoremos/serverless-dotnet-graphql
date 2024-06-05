namespace GraphQL.Database.Repositories.SessionSpeakers;

using ErrorOr;
using GraphQL.Database.Repositories.Sessions;
using GraphQL.Database.Repositories.Speakers;

public interface ISessionSpeakerRepository
{
    Task<IQueryable<SpeakerModel>> GetSpeakersBySessionAsync(int sessionId, CancellationToken ctx);

    Task<IQueryable<SessionModel>> GetSessionsBySpeakerAsync(int speakerId, CancellationToken ctx);

    Task<ErrorOr<Created>> CreateSessionSpeakerAsync(SessionSpeakerModelInput input, CancellationToken ctx);

    Task<ErrorOr<Deleted>> DeleteSessionSpeakerAsync(SessionSpeakerModelInput input, CancellationToken ctx);
}
