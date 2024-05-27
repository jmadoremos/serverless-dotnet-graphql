namespace GraphQL.Repositories.Database.Sessions;

using GraphQL.Schemas.Database.Sessions;
using System.ComponentModel.DataAnnotations;

public class SessionInput
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = default!;

    [StringLength(4000)]
    public string? Abstract { get; set; } = default!;

    public DateTimeOffset? StartTime { get; set; } = default!;

    public DateTimeOffset? EndTime { get; set; } = default!;

    public int TrackId { get; set; } = default!;

    public static SessionInput MapFrom(AddSessionSchema s) => new()
    {
        Title = s.Title,
        Abstract = s.Abstract,
        StartTime = s.StartTime,
        EndTime = s.EndTime,
        TrackId = s.TrackId
    };

    public static SessionInput MapFrom(Session o, UpdateSessionSchema s) => new()
    {
        Title = s.Title ?? o.Title,
        Abstract = s.Abstract ?? o.Abstract,
        StartTime = s.StartTime ?? o.StartTime,
        EndTime = s.EndTime ?? o.EndTime,
        TrackId = s.TrackId ?? o.TrackId
    };
}
