namespace GraphQL.StarWars.Schemas.Species;

using GraphQL.StarWars.Extensions;
using GraphQL.StarWars.Repositories;
using GraphQL.StarWars.Repositories.Responses;
using GraphQL.StarWars.Schemas.Films;
using GraphQL.StarWars.Schemas.Characters;
using GraphQL.StarWars.Schemas.Planets;

[Node]
[GraphQLDescription("A species resource is a type of person or character within the Star Wars Universe.")]
public class Species
{
    [ID]
    [GraphQLDescription("The unique identifier of this species.")]
    public int Id { get; set; }

    [GraphQLDescription("The name of this species.")]
    public string Name { get; set; } = default!;

    [GraphQLDescription("The classification of this species, such as \"mammal\" or \"reptile\".")]
    public string Classification { get; set; } = default!;

    [GraphQLDescription("The designation of this species, such as \"sentient\".")]
    public string Designation { get; set; } = default!;

    [GraphQLDescription("The average height of this species in centimeters.")]
    public string AverageHeight { get; set; } = default!;

    [GraphQLDescription("The average lifespan of this species in years.")]
    public string AverageLifespan { get; set; } = default!;

    [GraphQLDescription("A comma-separated string of common eye colors for this species, \"none\" if this species does not typically have eyes.")]
    public string EyeColors { get; set; } = default!;

    [GraphQLDescription("A comma-separated string of common hair colors for this species, \"none\" if this species does not typically have hair.")]
    public string HairColors { get; set; } = default!;

    [GraphQLDescription("A comma-separated string of common skin colors for this species, \"none\" if this species does not typically have skin.")]
    public string SkinColors { get; set; } = default!;

    [GraphQLDescription("The language commonly spoken by this species.")]
    public string Language { get; set; } = default!;

    [GraphQLIgnore]
    public int? HomeworldId { get; set; } = default!;

    [GraphQLDescription("A planet that this species originates from.")]
    public Planet? Homeworld { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> PersonIds { get; set; } = default!;

    [GraphQLDescription("A list of people that are a part of this species.")]
    public IEnumerable<Character> People { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> FilmIds { get; set; } = default!;

    [GraphQLDescription("A list of films that this species has appeared in.")]
    public IEnumerable<Film> Films { get; set; } = default!;

    [NodeResolver]
    public static async Task<Species?> GetNodeAsync(
        [ID(nameof(Species))] int id,
        [Service] IStarWarsRepository<SpeciesApiResponse> species,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await species.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return MapFrom(response);
    }

    public static Species MapFrom(SpeciesApiResponse r) => new()
    {
        Id = r.URL.ExtractSwapiId(),
        Name = r.Name,
        Classification = r.Classification,
        Designation = r.Designation,
        AverageHeight = r.AverageHeight,
        AverageLifespan = r.AverageLifespan,
        EyeColors = r.EyeColors,
        HairColors = r.HairColors,
        SkinColors = r.SkinColors,
        Language = r.Language,
        HomeworldId = r.Homeworld?.ExtractSwapiId(),
        PersonIds = r.People.Select(s => s.ExtractSwapiId()),
        FilmIds = r.Films.Select(s => s.ExtractSwapiId())
    };
}
