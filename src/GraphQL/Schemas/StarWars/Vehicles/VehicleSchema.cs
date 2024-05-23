namespace GraphQL.Schemas.StarWars.Vehicles;

using GraphQL.Extensions;
using GraphQL.Repositories.StarWars.Vehicles;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.People;

[GraphQLDescription("A vehicle resource is a single transport craft that does not have hyperdrive capability.")]
public class VehicleSchema
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Model { get; set; } = default!;

    public string VehicleClass { get; set; } = default!;

    public string Manufacturer { get; set; } = default!;

    public string Length { get; set; } = default!;

    public string CostInCredits { get; set; } = default!;

    public string Crew { get; set; } = default!;

    public string Passengers { get; set; } = default!;

    public string MaxAtmospheringSpeed { get; set; } = default!;

    public string HyperdriveRating { get; set; } = default!;

    public string MGLT { get; set; } = default!;

    public string CargoCapacity { get; set; } = default!;

    public string Consumables { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> FilmIds { get; set; } = default!;

    public IEnumerable<FilmSchema> Films { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PilotIds { get; set; } = default!;

    public IEnumerable<PersonSchema> Pilots { get; set; } = default!;

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
