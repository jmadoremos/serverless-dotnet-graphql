namespace GraphQL.Repositories.StarWars.Planets;

using Newtonsoft.Json;

public class PlanetApiResponse : SwapiResponse
{
    public string Name { get; set; } = default!;

    public string Diameter { get; set; } = default!;

    [JsonProperty("rotation_period")]
    public string RotationPeriod { get; set; } = default!;

    [JsonProperty("orbital_period")]
    public string OrbitalPeriod { get; set; } = default!;

    public string Gravity { get; set; } = default!;

    public string Population { get; set; } = default!;

    public string Climate { get; set; } = default!;

    public string Terrain { get; set; } = default!;

    [JsonProperty("surface_water")]
    public string SurfaceWater { get; set; } = default!;

    public IEnumerable<Uri> Residents { get; set; } = default!;

    public IEnumerable<Uri> Films { get; set; } = default!;
}
