namespace GraphQL.Schemas.StarWars.Films;

using GraphQL.Extensions;
using GraphQL.Repositories.StarWars.Films;
using GraphQL.Schemas.StarWars.People;
using GraphQL.Schemas.StarWars.Planets;
using GraphQL.Schemas.StarWars.Species;
using GraphQL.Schemas.StarWars.Starships;
using GraphQL.Schemas.StarWars.Vehicles;

[GraphQLDescription("A film resource is a single film.")]
public class FilmSchema
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public int EpisodeId { get; set; } = default!;

    public string OpeningCrawl { get; set; } = default!;

    public string Director { get; set; } = default!;

    public string Producer { get; set; } = default!;

    public string ReleaseDate { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PersonIds { get; set; } = default!;

    public IEnumerable<PersonSchema> Characters { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PlanetIds { get; set; } = default!;

    public IEnumerable<PlanetSchema> Planets { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> StarshipIds { get; set; } = default!;

    public IEnumerable<StarshipSchema> Starships { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> VehicleIds { get; set; } = default!;

    public IEnumerable<VehicleSchema> Vehicles { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> SpeciesIds { get; set; } = default!;

    public IEnumerable<SpeciesSchema> Species { get; set; } = default!;

    public static FilmSchema MapFrom(FilmApiResponse r) => new()
    {
        Id = r.URL.ExtractSwapiId(),
        Title = r.Title,
        EpisodeId = r.EpisodeId,
        OpeningCrawl = r.OpeningCrawl,
        Director = r.Director,
        Producer = r.Producer,
        ReleaseDate = r.ReleaseDate,
        PersonIds = r.Characters.Select(s => s.ExtractSwapiId()),
        PlanetIds = r.Planets.Select(s => s.ExtractSwapiId()),
        StarshipIds = r.Starships.Select(s => s.ExtractSwapiId()),
        VehicleIds = r.Vehicles.Select(s => s.ExtractSwapiId()),
        SpeciesIds = r.Species.Select(s => s.ExtractSwapiId())
    };
}
