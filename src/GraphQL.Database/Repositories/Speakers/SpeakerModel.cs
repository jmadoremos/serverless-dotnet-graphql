namespace GraphQL.Database.Repositories.Speakers;

using GraphQL.Database.Repositories.Sessions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Speakers")]
public class SpeakerModel : SpeakerModelInput
{
    [Key]
    public int Id { get; set; }

    public ICollection<SessionModel> Sessions { get; set; } = [];

    public static SpeakerModel MapFrom(SpeakerModelInput i) => new()
    {
        Name = i.Name,
        Bio = i.Bio,
        WebSite = i.WebSite
    };
}
