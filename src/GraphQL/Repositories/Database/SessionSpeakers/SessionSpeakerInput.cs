namespace GraphQL.Repositories.Database.SessionSpeakers;

using GraphQL.Schemas.Database.Sessions;

public class SessionSpeakerInput
{
    public int SessionId { get; set; }

    public int SpeakerId { get; set; }

    public static SessionSpeakerInput MapFrom(SessionSpeakerSchema s) => new()
    {
        SessionId = s.SessionId,
        SpeakerId = s.SpeakerId
    };
}
