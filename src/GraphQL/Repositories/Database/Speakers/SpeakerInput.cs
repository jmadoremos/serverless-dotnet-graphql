namespace GraphQL.Repositories.Database.Speakers;

using System.ComponentModel.DataAnnotations;
using GraphQL.Schemas.Database.Speakers;

public class SpeakerInput
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = default!;

    [StringLength(4000)]
    public string? Bio { get; set; } = default!;

    [StringLength(1000)]
    public string? WebSite { get; set; } = default!;

    public static SpeakerInput MapFrom(AddSpeakerSchema s) => new()
    {
        Name = s.Name,
        Bio = s.Name,
        WebSite = s.Website
    };

    public static SpeakerInput MapFrom(Speaker o, UpdateSpeakerSchema s) => new()
    {
        Name = s.Name ?? o.Name,
        Bio = s.Name ?? o.Bio,
        WebSite = s.Website ?? o.WebSite
    };
}
