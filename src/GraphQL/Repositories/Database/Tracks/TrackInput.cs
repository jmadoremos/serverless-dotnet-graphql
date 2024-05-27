namespace GraphQL.Repositories.Database.Tracks;

using GraphQL.Schemas.Database.Tracks;
using System.ComponentModel.DataAnnotations;

public class TrackInput
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = default!;

    public static TrackInput MapFrom(AddTrackSchema s) => new()
    {
        Name = s.Name
    };

    public static TrackInput MapFrom(Track o, UpdateTrackSchema s) => new()
    {
        Name = s.Name ?? o.Name
    };
}
