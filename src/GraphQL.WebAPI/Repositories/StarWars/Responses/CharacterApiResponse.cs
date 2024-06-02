namespace GraphQL.WebAPI.Repositories.StarWars.Responses;

using Newtonsoft.Json;

public class CharacterApiResponse : SwapiResponse
{
    public string Name { get; set; } = default!;

    public string Height { get; set; } = default!;

    public string Mass { get; set; } = default!;

    [JsonProperty("hair_color")]
    public string HairColor { get; set; } = default!;

    [JsonProperty("skin_color")]
    public string SkinColor { get; set; } = default!;

    [JsonProperty("eye_color")]
    public string EyeColor { get; set; } = default!;

    [JsonProperty("birth_year")]
    public string BirthYear { get; set; } = default!;

    public string Gender { get; set; } = default!;

    public Uri? Homeworld { get; set; } = default!;

    public IEnumerable<Uri> Films { get; set; } = default!;

    public IEnumerable<Uri> Species { get; set; } = default!;

    public IEnumerable<Uri> Vehicles { get; set; } = default!;

    public IEnumerable<Uri> Starships { get; set; } = default!;
}
