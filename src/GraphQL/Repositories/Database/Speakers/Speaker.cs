namespace GraphQL.Repositories.Database.Speakers;

using GraphQL.Repositories.Database.SessionSpeakerMapping;

public class Speaker : SpeakerInput
{
    public int Id { get; set; }

    public ICollection<SessionSpeakerMapping> SessionSpeakers { get; set; } = [];

    public static Speaker MapFrom(SpeakerInput i) => new()
    {
        Name = i.Name,
        Bio = i.Bio,
        WebSite = i.WebSite
    };
}
