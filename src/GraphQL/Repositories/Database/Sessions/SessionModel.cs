namespace GraphQL.Repositories.Database.Sessions;

using GraphQL.Repositories.Database.Attendees;
using GraphQL.Repositories.Database.Speakers;
using GraphQL.Repositories.Database.Tracks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Sessions")]
public class SessionModel : SessionModelInput
{
    [Key]
    public int Id { get; set; }

    public ICollection<AttendeeModel> Attendees { get; set; } = [];

    public ICollection<SpeakerModel> Speakers { get; set; } = [];

    public TrackModel Track { get; set; } = default!;

    public static SessionModel MapFrom(SessionModelInput i) => new()
    {
        Title = i.Title,
        Abstract = i.Abstract,
        StartTime = i.StartTime,
        EndTime = i.EndTime,
        TrackId = i.TrackId
    };
}
