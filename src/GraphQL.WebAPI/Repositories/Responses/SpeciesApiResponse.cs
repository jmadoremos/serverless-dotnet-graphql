namespace GraphQL.WebAPI.Repositories.Responses;

using Newtonsoft.Json;

public class SpeciesApiResponse : SwapiResponse
{
    public string Name { get; set; } = default!;

    public string Classification { get; set; } = default!;

    public string Designation { get; set; } = default!;

    [JsonProperty("average_height")]
    public string AverageHeight { get; set; } = default!;

    [JsonProperty("average_lifespan")]
    public string AverageLifespan { get; set; } = default!;

    [JsonProperty("eye_colors")]
    public string EyeColors { get; set; } = default!;

    [JsonProperty("hair_colors")]
    public string HairColors { get; set; } = default!;

    [JsonProperty("skin_colors")]
    public string SkinColors { get; set; } = default!;

    public string Language { get; set; } = default!;

    public Uri? Homeworld { get; set; } = default!;

    public IEnumerable<Uri> People { get; set; } = default!;

    public IEnumerable<Uri> Films { get; set; } = default!;
}
