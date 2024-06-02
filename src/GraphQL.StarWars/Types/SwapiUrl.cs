namespace GraphQL.StarWars.Types;

public sealed class SwapiUrl
{
    public string BaseUrl { get; private set; }

    public Uri BaseUri { get; private set; }

    private SwapiUrl(string baseUrl)
    {
        baseUrl = baseUrl.Trim();
        this.BaseUrl = baseUrl;
        this.BaseUri = new Uri(baseUrl);
    }

    public override string ToString() =>
        this.BaseUrl;

    public static readonly SwapiUrl CharactersUrl = new("https://swapi.dev/api/people/");

    public static readonly SwapiUrl FilmsUrl = new("https://swapi.dev/api/films/");

    public static readonly SwapiUrl PlanetsUrl = new("https://swapi.dev/api/planets/");

    public static readonly SwapiUrl SpeciesUrl = new("https://swapi.dev/api/species/");

    public static readonly SwapiUrl StarshipsUrl = new("https://swapi.dev/api/starships/");

    public static readonly SwapiUrl VehiclesUrl = new("https://swapi.dev/api/vehicles/");
}
