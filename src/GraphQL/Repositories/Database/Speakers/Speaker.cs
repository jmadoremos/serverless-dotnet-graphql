namespace GraphQL.Repositories.Database.Speakers;

using GraphQL.Repositories.Database.Sessions;

public class Speaker : SpeakerInput
{
    public int Id { get; set; }

    public ICollection<Session> Sessions { get; set; } = [];

    public static Speaker MapFrom(SpeakerInput i) => new()
    {
        Name = i.Name,
        Bio = i.Bio,
        WebSite = i.WebSite
    };
}
