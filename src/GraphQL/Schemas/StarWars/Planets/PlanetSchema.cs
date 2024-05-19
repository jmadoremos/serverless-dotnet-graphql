namespace GraphQL.Schemas.StarWars.Planets;

using GraphQL.Extensions;
using GraphQL.Repositories.StarWars.Planets;

[GraphQLDescription("A planet resource is a large mass, planet or planetoid in the Star Wars Universe, at the time of 0 ABY.")]
public class PlanetSchema
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string RotationPeriod { get; set; } = default!;

    public string OrbitalPeriod { get; set; } = default!;

    public string Diameter { get; set; } = default!;

    public string Climate { get; set; } = default!;

    public string Gravity { get; set; } = default!;

    public string Terrain { get; set; } = default!;

    public string SurfaceWater { get; set; } = default!;

    public string Population { get; set; } = default!;

    public static PlanetSchema MapFrom(PlanetApiResponse r) => new()
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
        Population = r.Population
    };
}
