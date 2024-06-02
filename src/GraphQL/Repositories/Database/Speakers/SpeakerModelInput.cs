namespace GraphQL.Repositories.Database.Speakers;

using GraphQL.Schemas.Database.Speakers;
using System.ComponentModel.DataAnnotations;

public class SpeakerModelInput
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = default!;

    [StringLength(4000)]
    public string? Bio { get; set; } = default!;

    [StringLength(1000)]
    public string? WebSite { get; set; } = default!;

    public static SpeakerModelInput MapFrom(AddSpeakerInput s) => new()
    {
        Name = s.Name,
        Bio = s.Name,
        WebSite = s.Website
    };

    public static SpeakerModelInput MapFrom(SpeakerModel o, UpdateSpeakerInput s) => new()
    {
        Name = s.Name ?? o.Name,
        Bio = s.Name ?? o.Bio,
        WebSite = s.Website ?? o.WebSite
    };
}
