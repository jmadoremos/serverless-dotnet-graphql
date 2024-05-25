namespace GraphQL.Schemas.StarWars.Films;

using GraphQL.Extensions;
using GraphQL.Repositories.StarWars.Films;
using GraphQL.Schemas.StarWars.Characters;
using GraphQL.Schemas.StarWars.Planets;
using GraphQL.Schemas.StarWars.Species;
using GraphQL.Schemas.StarWars.Starships;
using GraphQL.Schemas.StarWars.Vehicles;

[GraphQLDescription("A film resource is a single film.")]
public class FilmSchema
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; }

    [GraphQLDescription("The title of this film.")]
    public string Title { get; set; } = default!;

    [GraphQLDescription("The episode number of this film.")]
    public int EpisodeId { get; set; } = default!;

    [GraphQLDescription("The opening paragraphs at the beginning of this film.")]
    public string OpeningCrawl { get; set; } = default!;

    [GraphQLDescription("The name of the director of this film.")]
    public string Director { get; set; } = default!;

    [GraphQLDescription(" The name(s) of the producer(s) of this film. Comma separated.")]
    public string Producer { get; set; } = default!;

    [GraphQLDescription("The ISO 8601 date format of film release at original creator country.")]
    public string ReleaseDate { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> SpeciesIds { get; set; } = default!;

    [GraphQLDescription("A list of species that are in this film.")]
    public IEnumerable<SpeciesSchema> Species { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> StarshipIds { get; set; } = default!;

    [GraphQLDescription("A list of starships that are in this film.")]
    public IEnumerable<StarshipSchema> Starships { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> VehicleIds { get; set; } = default!;

    [GraphQLDescription("A list of vehicles that are in this film.")]
    public IEnumerable<VehicleSchema> Vehicles { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PersonIds { get; set; } = default!;

    [GraphQLDescription("A list of characters that are in this film.")]
    public IEnumerable<CharacterSchema> Characters { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PlanetIds { get; set; } = default!;

    [GraphQLDescription("A list of planets that are in this film.")]
    public IEnumerable<PlanetSchema> Planets { get; set; } = default!;

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
