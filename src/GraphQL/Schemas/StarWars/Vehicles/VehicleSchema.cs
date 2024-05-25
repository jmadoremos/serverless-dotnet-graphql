namespace GraphQL.Schemas.StarWars.Vehicles;

using GraphQL.Extensions;
using GraphQL.Repositories.StarWars.Vehicles;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.Characters;

[GraphQLDescription("A vehicle resource is a single transport craft that does not have hyperdrive capability.")]
public class VehicleSchema
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; }

    [GraphQLDescription("The name of this vehicle. The common name, such as \"Sand Crawler\" or \"Speeder bike\".")]
    public string Name { get; set; } = default!;

    [GraphQLDescription("The model or official name of this vehicle. Such as \"All-Terrain Attack Transport\".")]
    public string Model { get; set; } = default!;

    [GraphQLDescription("The class of this vehicle, such as \"Wheeled\" or \"Repulsorcraft\".")]
    public string VehicleClass { get; set; } = default!;

    [GraphQLDescription("The manufacturer of this vehicle. Comma separated if more than one.")]
    public string Manufacturer { get; set; } = default!;

    [GraphQLDescription("The length of this vehicle in meters.")]
    public string Length { get; set; } = default!;

    [GraphQLDescription("The cost of this vehicle new, in Galactic Credits.")]
    public string CostInCredits { get; set; } = default!;

    [GraphQLDescription("The number of personnel needed to run or pilot this vehicle.")]
    public string Crew { get; set; } = default!;

    [GraphQLDescription("The number of non-essential people this vehicle can transport.")]
    public string Passengers { get; set; } = default!;

    [GraphQLDescription("The maximum speed of this vehicle in the atmosphere.")]
    public string MaxAtmospheringSpeed { get; set; } = default!;

    [GraphQLDescription("The maximum number of kilograms that this vehicle can transport.")]
    public string CargoCapacity { get; set; } = default!;

    [GraphQLDescription("The maximum length of time that this vehicle can provide consumables for its entire crew without having to resupply.")]
    public string Consumables { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> FilmIds { get; set; } = default!;

    [GraphQLDescription("A list of films that this vehicle has appeared in.")]
    public IEnumerable<FilmSchema> Films { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PilotIds { get; set; } = default!;

    [GraphQLDescription("A list of pilots that this vehicle has been piloted by.")]
    public IEnumerable<CharacterSchema> Pilots { get; set; } = default!;

    public static VehicleSchema MapFrom(VehicleApiResponse r) => new()
    {
        Id = r.URL.ExtractSwapiId(),
        Name = r.Name,
        Model = r.Model,
        VehicleClass = r.VehicleClass,
        Manufacturer = r.Manufacturer,
        Length = r.Length,
        CostInCredits = r.CostInCredits,
        Crew = r.Crew,
        Passengers = r.Passengers,
        MaxAtmospheringSpeed = r.MaxAtmospheringSpeed,
        CargoCapacity = r.CargoCapacity,
        Consumables = r.Consumables,
        FilmIds = r.Films.Select(s => s.ExtractSwapiId()),
        PilotIds = r.Pilots.Select(s => s.ExtractSwapiId())
    };
}
