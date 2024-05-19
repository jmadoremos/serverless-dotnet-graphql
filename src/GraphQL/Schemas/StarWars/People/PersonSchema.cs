namespace GraphQL.Schemas.StarWars.People;

using GraphQL.Extensions;
using GraphQL.Repositories.StarWars.People;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.Planets;

[GraphQLDescription("A people resource is an individual person or character within the Star Wars universe.")]
public class PersonSchema
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Height { get; set; } = default!;

    public string Mass { get; set; } = default!;

    public string HairColor { get; set; } = default!;

    public string SkinColor { get; set; } = default!;

    public string EyeColor { get; set; } = default!;

    public string BirthYear { get; set; } = default!;

    public string Gender { get; set; } = default!;

    public string Created { get; set; } = default!;

    public string Edited { get; set; } = default!;

    public PlanetSchema? Homeworld { get; set; } = default!;

    public IEnumerable<FilmSchema>? Films { get; set; } = default!;

    public static PersonSchema MapFrom(PersonApiResponse r) => new()
    {
        Id = r.URL.ExtractSwapiId(),
        Name = r.Name,
        Height = r.Height,
        Mass = r.Mass,
        HairColor = r.HairColor,
        SkinColor = r.SkinColor,
        EyeColor = r.EyeColor,
        BirthYear = r.BirthYear,
        Gender = r.Gender,
        Created = r.Created,
        Edited = r.Edited,
        Homeworld = null,
        Films = null
    };
}
