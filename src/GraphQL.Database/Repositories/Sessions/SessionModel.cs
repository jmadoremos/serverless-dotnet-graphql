namespace GraphQL.Database.Repositories.Sessions;

using GraphQL.Database.Repositories.Attendees;
using GraphQL.Database.Repositories.Speakers;
using GraphQL.Database.Repositories.Tracks;
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
