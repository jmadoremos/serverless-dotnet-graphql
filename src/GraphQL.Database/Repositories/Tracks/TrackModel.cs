namespace GraphQL.Database.Repositories.Tracks;

using GraphQL.Database.Repositories.Sessions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Tracks")]
public class TrackModel : TrackModelInput
{
    [Key]
    public int Id { get; set; }

    public ICollection<SessionModel> Sessions { get; set; } = [];

    public static TrackModel MapFrom(TrackModelInput i) => new()
    {
        Name = i.Name
    };
}
