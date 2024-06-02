namespace GraphQL.WebAPI.Schemas.Films;

using GraphQL.WebAPI.Extensions;
using GraphQL.WebAPI.Repositories;
using GraphQL.WebAPI.Repositories.Responses;
using GraphQL.WebAPI.Schemas.Characters;
using GraphQL.WebAPI.Schemas.Planets;
using GraphQL.WebAPI.Schemas.Species;
using GraphQL.WebAPI.Schemas.Starships;
using GraphQL.WebAPI.Schemas.Vehicles;

[Node]
[GraphQLDescription("A film resource is a single film.")]
public class Film
{
    [ID]
    [GraphQLDescription("The unique identifier of this film.")]
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
    public IEnumerable<Species> Species { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> StarshipIds { get; set; } = default!;

    [GraphQLDescription("A list of starships that are in this film.")]
    public IEnumerable<Starship> Starships { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> VehicleIds { get; set; } = default!;

    [GraphQLDescription("A list of vehicles that are in this film.")]
    public IEnumerable<Vehicle> Vehicles { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PersonIds { get; set; } = default!;

    [GraphQLDescription("A list of characters that are in this film.")]
    public IEnumerable<Character> Characters { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PlanetIds { get; set; } = default!;

    [GraphQLDescription("A list of planets that are in this film.")]
    public IEnumerable<Planet> Planets { get; set; } = default!;

    [NodeResolver]
    public static async Task<Film?> GetNodeAsync(
        [ID(nameof(Film))] int id,
        [Service] IStarWarsRepository<FilmApiResponse> films,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await films.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return MapFrom(response);
    }

    public static Film MapFrom(FilmApiResponse r) => new()
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
