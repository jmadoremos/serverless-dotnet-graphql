namespace GraphQL.WebAPI.Repositories.StarWars.Responses;

using Newtonsoft.Json;

public class VehicleApiResponse : SwapiResponse
{
    public string Name { get; set; } = default!;

    public string Model { get; set; } = default!;

    [JsonProperty("vehicle_class")]
    public string VehicleClass { get; set; } = default!;

    public string Manufacturer { get; set; } = default!;

    public string Length { get; set; } = default!;

    [JsonProperty("cost_in_credits")]
    public string CostInCredits { get; set; } = default!;

    public string Crew { get; set; } = default!;

    public string Passengers { get; set; } = default!;

    [JsonProperty("max_atmosphering_speed")]
    public string MaxAtmospheringSpeed { get; set; } = default!;

    [JsonProperty("cargo_capacity")]
    public string CargoCapacity { get; set; } = default!;

    public string Consumables { get; set; } = default!;

    public IEnumerable<Uri> Films { get; set; } = default!;

    public IEnumerable<Uri> Pilots { get; set; } = default!;
}
