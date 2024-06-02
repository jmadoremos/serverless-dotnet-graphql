namespace GraphQL.WebAPI.Schemas.StarWars.Starships;

using GraphQL.WebAPI.Extensions;
using GraphQL.WebAPI.Schemas.StarWars.Films;
using GraphQL.WebAPI.Schemas.StarWars.Characters;
using GraphQL.WebAPI.Repositories.StarWars.Responses;
using GraphQL.WebAPI.Repositories.StarWars;

[Node]
[GraphQLDescription("A starship resource is a single transport craft that has hyperdrive capability.")]
public class Starship
{
    [ID]
    [GraphQLDescription("The unique identifier of this starship.")]
    public int Id { get; set; }

    [GraphQLDescription("The name of this starship. The common name, such as \"Death Star\".")]
    public string Name { get; set; } = default!;

    [GraphQLDescription("The model or official name of this starship. Such as \"T-65 X-wing\" or \"DS-1 Orbital Battle Station\".")]
    public string Model { get; set; } = default!;

    [GraphQLDescription("The class of this starship, such as \"Starfighter\" or \"Deep Space Mobile Battlestation\"")]
    public string StarshipClass { get; set; } = default!;

    [GraphQLDescription("The manufacturer of this starship. Comma separated if more than one.")]
    public string Manufacturer { get; set; } = default!;

    [GraphQLDescription("The cost of this starship new, in galactic credits.")]
    public string CostInCredits { get; set; } = default!;

    [GraphQLDescription("The length of this starship in meters.")]
    public string Length { get; set; } = default!;

    [GraphQLDescription("The number of personnel needed to run or pilot this starship.")]
    public string Crew { get; set; } = default!;

    [GraphQLDescription("The number of non-essential people this starship can transport.")]
    public string Passengers { get; set; } = default!;

    [GraphQLDescription("The maximum speed of this starship in the atmosphere. \"N/A\" if this starship is incapable of atmospheric flight.")]
    public string MaxAtmospheringSpeed { get; set; } = default!;

    [GraphQLDescription("The class of this starships hyperdrive.")]
    public string HyperdriveRating { get; set; } = default!;

    [GraphQLDescription("The Maximum number of Megalights this starship can travel in a standard hour. A \"Megalight\" is a standard unit of distance and has never been defined before within the Star Wars universe. This figure is only really useful for measuring the difference in speed of starships. We can assume it is similar to AU, the distance between our Sun (Sol) and Earth.")]
    public string MGLT { get; set; } = default!;

    [GraphQLDescription("The maximum number of kilograms that this starship can transport.")]
    public string CargoCapacity { get; set; } = default!;

    [GraphQLDescription("The maximum length of time that this starship can provide consumables for its entire crew without having to resupply.")]
    public string Consumables { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> FilmIds { get; set; } = default!;

    [GraphQLDescription("A list of films that this starship has appeared in.")]
    public IEnumerable<Film> Films { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PilotIds { get; set; } = default!;

    [GraphQLDescription("A list of films that this starship has been piloted by.")]
    public IEnumerable<Character> Pilots { get; set; } = default!;

    [NodeResolver]
    public static async Task<Starship?> GetNodeAsync(
        [ID(nameof(Starship))] int id,
        [Service] IStarWarsRepository<StarshipApiResponse> starships,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await starships.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return MapFrom(response);
    }

    public static Starship MapFrom(StarshipApiResponse r) => new()
    {
        Id = r.URL.ExtractSwapiId(),
        Name = r.Name,
        Model = r.Model,
        StarshipClass = r.StarshipClass,
        Manufacturer = r.Manufacturer,
        CostInCredits = r.CostInCredits,
        Length = r.Length,
        Crew = r.Crew,
        Passengers = r.Passengers,
        MaxAtmospheringSpeed = r.MaxAtmospheringSpeed,
        HyperdriveRating = r.HyperdriveRating,
        MGLT = r.MGLT,
        CargoCapacity = r.CargoCapacity,
        Consumables = r.Consumables,
        FilmIds = r.Films.Select(s => s.ExtractSwapiId()),
        PilotIds = r.Pilots.Select(s => s.ExtractSwapiId())
    };
}
