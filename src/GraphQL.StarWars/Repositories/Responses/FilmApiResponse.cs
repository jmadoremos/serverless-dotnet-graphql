namespace GraphQL.StarWars.Repositories.Responses;

using Newtonsoft.Json;

public class FilmApiResponse : SwapiResponse
{
    public string Title { get; set; } = default!;

    [JsonProperty("episode_id")]
    public int EpisodeId { get; set; } = default!;

    [JsonProperty("opening_crawl")]
    public string OpeningCrawl { get; set; } = default!;

    public string Director { get; set; } = default!;

    public string Producer { get; set; } = default!;

    [JsonProperty("release_date")]
    public string ReleaseDate { get; set; } = default!;

    public IEnumerable<Uri> Characters { get; set; } = default!;

    public IEnumerable<Uri> Planets { get; set; } = default!;

    public IEnumerable<Uri> Starships { get; set; } = default!;

    public IEnumerable<Uri> Vehicles { get; set; } = default!;

    public IEnumerable<Uri> Species { get; set; } = default!;
}
