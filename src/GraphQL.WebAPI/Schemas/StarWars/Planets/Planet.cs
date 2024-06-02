namespace GraphQL.WebAPI.Schemas.StarWars.Planets;

using GraphQL.WebAPI.Extensions;
using GraphQL.WebAPI.Schemas.StarWars.Films;
using GraphQL.WebAPI.Schemas.StarWars.Characters;
using GraphQL.WebAPI.Repositories.StarWars.Responses;
using GraphQL.WebAPI.Repositories.StarWars;

[Node]
[GraphQLDescription("A planet resource is a large mass, planet or planetoid in the Star Wars Universe, at the time of 0 ABY.")]
public class Planet
{
    [ID]
    [GraphQLDescription("The unique identifier of this planet.")]
    public int Id { get; set; }

    [GraphQLDescription("The name of this planet.")]
    public string Name { get; set; } = default!;

    [GraphQLDescription("The diameter of this planet in kilometers.")]
    public string Diameter { get; set; } = default!;

    [GraphQLDescription("The number of standard hours it takes for this planet to complete a single rotation on its axis.")]
    public string RotationPeriod { get; set; } = default!;

    [GraphQLDescription("The number of standard days it takes for this planet to complete a single orbit of its local star.")]
    public string OrbitalPeriod { get; set; } = default!;

    [GraphQLDescription("A number denoting the gravity of this planet, where \"1\" is normal or 1 standard G. \"2\" is twice or 2 standard Gs. \"0.5\" is half or 0.5 standard Gs.")]
    public string Gravity { get; set; } = default!;

    [GraphQLDescription("The average population of sentient beings inhabiting this planet.")]
    public string Population { get; set; } = default!;

    [GraphQLDescription("The climate of this planet. Comma separated if diverse.")]
    public string Climate { get; set; } = default!;

    [GraphQLDescription("The terrain of this planet. Comma separated if diverse.")]
    public string Terrain { get; set; } = default!;

    [GraphQLDescription("The percentage of the planet surface that is naturally occurring water or bodies of water.")]
    public string SurfaceWater { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PersonIds { get; set; } = default!;

    [GraphQLDescription("A list of residents that live on this planet.")]
    public IEnumerable<Character> Residents { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> FilmIds { get; set; } = default!;

    [GraphQLDescription("A list of films that this planet has appeared in.")]
    public IEnumerable<Film> Films { get; set; } = default!;

    [NodeResolver]
    public static async Task<Planet?> GetNodeAsync(
        [ID(nameof(Planet))] int id,
        [Service] IStarWarsRepository<PlanetApiResponse> planets,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await planets.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return MapFrom(response);
    }

    public static Planet MapFrom(PlanetApiResponse r) => new()
    {
        Id = r.URL.ExtractSwapiId(),
        Name = r.Name,
        RotationPeriod = r.RotationPeriod,
        OrbitalPeriod = r.OrbitalPeriod,
        Diameter = r.Diameter,
        Climate = r.Climate,
        Gravity = r.Gravity,
        Terrain = r.Terrain,
        SurfaceWater = r.SurfaceWater,
        Population = r.Population,
        PersonIds = r.Residents.Select(s => s.ExtractSwapiId()),
        FilmIds = r.Films.Select(s => s.ExtractSwapiId())
    };
}
