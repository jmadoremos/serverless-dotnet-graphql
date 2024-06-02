namespace GraphQL.Database.Repositories.SessionSpeakers;

using GraphQL.Database.Schemas.Sessions;

public class SessionSpeakerModelInput
{
    public int SessionId { get; set; }

    public int SpeakerId { get; set; }

    public static SessionSpeakerModelInput MapFrom(AddRemoveSessionSpeaker s) => new()
    {
        SessionId = s.SessionId,
        SpeakerId = s.SpeakerId
    };
}
