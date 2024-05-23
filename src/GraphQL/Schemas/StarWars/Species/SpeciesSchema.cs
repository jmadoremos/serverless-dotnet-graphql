namespace GraphQL.Schemas.StarWars.Species;

using GraphQL.Extensions;
using GraphQL.Repositories.StarWars.Species;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.People;
using GraphQL.Schemas.StarWars.Planets;

[GraphQLDescription("A species resource is a type of person or character within the Star Wars Universe.")]
public class SpeciesSchema
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Classification { get; set; } = default!;

    public string Designation { get; set; } = default!;

    public string AverageHeight { get; set; } = default!;

    public string AverageLifespan { get; set; } = default!;

    public string EyeColors { get; set; } = default!;

    public string HairColors { get; set; } = default!;

    public string SkinColors { get; set; } = default!;

    public string Language { get; set; } = default!;

    [GraphQLIgnore]
    public int? HomeworldId { get; set; } = default!;

    public PlanetSchema? Homeworld { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PersonIds { get; set; } = default!;

    public IEnumerable<PersonSchema> People { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> FilmIds { get; set; } = default!;

    public IEnumerable<FilmSchema> Films { get; set; } = default!;

    public static SpeciesSchema MapFrom(SpeciesApiResponse r) => new()
    {
        Id = r.URL.ExtractSwapiId(),
        Name = r.Name,
        Classification = r.Classification,
        Designation = r.Designation,
        AverageHeight = r.AverageHeight,
        AverageLifespan = r.AverageLifespan,
        EyeColors = r.EyeColors,
        HairColors = r.HairColors,
        SkinColors = r.SkinColors,
        Language = r.Language,
        HomeworldId = r.Homeworld?.ExtractSwapiId(),
        PersonIds = r.People.Select(s => s.ExtractSwapiId()),
        FilmIds = r.Films.Select(s => s.ExtractSwapiId())
    };
}
