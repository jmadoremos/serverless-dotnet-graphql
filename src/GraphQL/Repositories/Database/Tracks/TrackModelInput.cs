namespace GraphQL.Repositories.Database.Tracks;

using GraphQL.Schemas.Database.Tracks;
using System.ComponentModel.DataAnnotations;

public class TrackModelInput
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = default!;

    public static TrackModelInput MapFrom(AddTrackInput s) => new()
    {
        Name = s.Name
    };

    public static TrackModelInput MapFrom(TrackModel o, UpdateTrackInput s) => new()
    {
        Name = s.Name ?? o.Name
    };
}
