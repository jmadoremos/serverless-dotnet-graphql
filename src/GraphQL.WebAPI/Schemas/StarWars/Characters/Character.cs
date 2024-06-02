namespace GraphQL.WebAPI.Schemas.StarWars.Characters;

using GraphQL.WebAPI.Extensions;
using GraphQL.WebAPI.Repositories.StarWars;
using GraphQL.WebAPI.Repositories.StarWars.Responses;
using GraphQL.WebAPI.Schemas.StarWars.Films;
using GraphQL.WebAPI.Schemas.StarWars.Planets;
using GraphQL.WebAPI.Schemas.StarWars.Species;
using GraphQL.WebAPI.Schemas.StarWars.Starships;
using GraphQL.WebAPI.Schemas.StarWars.Vehicles;

[Node]
[GraphQLDescription("A character resource is an individual person or character within the Star Wars universe.")]
public class Character
{
    [ID]
    [GraphQLDescription("The unique identifier of this character.")]
    public int Id { get; set; }

    [GraphQLDescription("The name of this person.")]
    public string Name { get; set; } = default!;

    [GraphQLDescription("The birth year of the person, using the in-universe standard of BBY or ABY - Before the Battle of Yavin or After the Battle of Yavin. The Battle of Yavin is a battle that occurs at the end of Star Wars episode IV: A New Hope.")]
    public string BirthYear { get; set; } = default!;

    [GraphQLDescription("The eye color of this person. Will be \"unknown\" if not known or \"n/a\" if the person does not have an eye.")]
    public string EyeColor { get; set; } = default!;

    [GraphQLDescription("The gender of this person. Either \"Male\", \"Female\" or \"unknown\", \"n/a\" if the person does not have a gender.")]
    public string Gender { get; set; } = default!;

    [GraphQLDescription("The hair color of this person. Will be \"unknown\" if not known or \"n/a\" if the person does not have hair.")]
    public string HairColor { get; set; } = default!;

    [GraphQLDescription("The height of the person in centimeters.")]
    public string Height { get; set; } = default!;

    [GraphQLDescription("The mass of the person in kilograms.")]
    public string Mass { get; set; } = default!;

    [GraphQLDescription("The skin color of this person.")]
    public string SkinColor { get; set; } = default!;

    [GraphQLIgnore]
    public int? HomeworldId { get; set; } = default!;

    [GraphQLDescription("A planet that this person was born on or inhabits.")]
    public Planet? Homeworld { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> FilmIds { get; set; } = default!;

    [GraphQLDescription("A list of films that this person has been in.")]
    public IEnumerable<Film> Films { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> SpeciesIds { get; set; } = default!;

    [GraphQLDescription("A list of species that this person belongs to.")]
    public IEnumerable<Species> Species { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> StarshipIds { get; set; } = default!;

    [GraphQLDescription("A list of starships that this person has piloted.")]
    public IEnumerable<Starship> Starships { get; set; } = default!;

    [GraphQLIgnore]
    public IEnumerable<int> VehicleIds { get; set; } = default!;

    [GraphQLDescription("A list of vehicles that this person has piloted.")]
    public IEnumerable<Vehicle> Vehicles { get; set; } = default!;

    [NodeResolver]
    public static async Task<Character?> GetNodeAsync(
        [ID(nameof(Character))] int id,
        [Service] IStarWarsRepository<CharacterApiResponse> films,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await films.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return MapFrom(response);
    }

    public static Character MapFrom(CharacterApiResponse r) => new()
    {
        Id = r.URL.ExtractSwapiId(),
        Name = r.Name,
        Height = r.Height,
        Mass = r.Mass,
        HairColor = r.HairColor,
        SkinColor = r.SkinColor,
        EyeColor = r.EyeColor,
        BirthYear = r.BirthYear,
        Gender = r.Gender,
        HomeworldId = r.Homeworld?.ExtractSwapiId(),
        FilmIds = r.Films.Select(s => s.ExtractSwapiId()),
        SpeciesIds = r.Species.Select(s => s.ExtractSwapiId()),
        StarshipIds = r.Starships.Select(s => s.ExtractSwapiId()),
        VehicleIds = r.Vehicles.Select(s => s.ExtractSwapiId())
    };
}
