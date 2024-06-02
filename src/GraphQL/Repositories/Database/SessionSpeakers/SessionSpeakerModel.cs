namespace GraphQL.Repositories.Database.SessionSpeakers;

using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.Speakers;
using System.ComponentModel.DataAnnotations.Schema;

[Table("SessionSpeakerMapping")]
public class SessionSpeakerModel : SessionSpeakerModelInput
{
    public SessionModel Session { get; set; } = default!;

    public SpeakerModel Speaker { get; set; } = default!;

    public static SessionSpeakerModel MapFrom(SessionSpeakerModelInput i) => new()
    {
        SessionId = i.SessionId,
        SpeakerId = i.SpeakerId
    };
}
