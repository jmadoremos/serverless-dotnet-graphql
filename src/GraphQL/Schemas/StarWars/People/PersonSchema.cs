namespace GraphQL.Schemas.StarWars.People;

using GraphQL.Extensions;
using GraphQL.Repositories.StarWars.People;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.Planets;
using GraphQL.Schemas.StarWars.Starships;
using GraphQL.Schemas.StarWars.Vehicles;

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

    [GraphQLIgnore]
    public int HomeworldId { get; set; } = default!;

    public PlanetSchema Homeworld { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> FilmIds { get; set; } = default!;

    public IEnumerable<FilmSchema> Films { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> StarshipIds { get; set; } = default!;

    public IEnumerable<StarshipSchema> Starships { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> VehicleIds { get; set; } = default!;

    public IEnumerable<VehicleSchema> Vehicles { get; set; } = default!;

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
        HomeworldId = r.Homeworld.ExtractSwapiId(),
        FilmIds = r.Films.Select(s => s.ExtractSwapiId()),
        StarshipIds = r.Starships.Select(s => s.ExtractSwapiId()),
        VehicleIds = r.Vehicles.Select(s => s.ExtractSwapiId())
    };
}
