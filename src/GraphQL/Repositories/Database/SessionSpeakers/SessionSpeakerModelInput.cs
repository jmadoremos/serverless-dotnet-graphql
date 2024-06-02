namespace GraphQL.Repositories.Database.SessionSpeakers;

using GraphQL.Schemas.Database.Sessions;

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
