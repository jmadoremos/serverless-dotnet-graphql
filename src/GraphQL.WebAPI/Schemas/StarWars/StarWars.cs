namespace GraphQL.WebAPI.Schemas.StarWars;

using GraphQL.WebAPI.Schemas.StarWars.Characters;
using GraphQL.WebAPI.Schemas.StarWars.Films;
using GraphQL.WebAPI.Schemas.StarWars.Planets;
using GraphQL.WebAPI.Schemas.StarWars.Starships;
using GraphQL.WebAPI.Schemas.StarWars.Vehicles;

[GraphQLDescription("All the Star Wars data you've ever wanted.")]
public class StarWars
{
    [GraphQLDescription("The base URL of the Star Wars API.")]
    public Uri BaseUrl { get; set; } = default!;

    [GraphQLDescription("A list of people in Star Wars Universe.")]
    public IEnumerable<Character> Characters { get; set; } = [];

    [GraphQLDescription("A person or character in Star Wars Universe identified by the Star Wars API identifier.")]
    public Character CharacterById { get; set; } = default!;

    [GraphQLDescription("A list of films in Star Wars Universe.")]
    public IEnumerable<Film> Films { get; set; } = [];

    [GraphQLDescription("A film in Star Wars Universe identified by the Star Wars API identifier.")]
    public Film FilmById { get; set; } = default!;

    [GraphQLDescription("A list of planets in Star Wars Universe.")]
    public IEnumerable<Planet> Planets { get; set; } = [];

    [GraphQLDescription("A planet in Star Wars Universe identified by the Star Wars API identifier.")]
    public Planet PlanetById { get; set; } = default!;

    [GraphQLDescription("A list of species in Star Wars Universe.")]
    public IEnumerable<Species.Species> Species { get; set; } = [];

    [GraphQLDescription("A species in Star Wars Universe identified by the Star Wars API identifier.")]
    public Species.Species SpeciesById { get; set; } = default!;

    [GraphQLDescription("A list of starships in Star Wars Universe.")]
    public IEnumerable<Starship> Starships { get; set; } = [];

    [GraphQLDescription("A starship in Star Wars Universe identified by the Star Wars API identifier.")]
    public Starship StarshipById { get; set; } = default!;

    [GraphQLDescription("A list of vehicle in Star Wars Universe.")]
    public IEnumerable<Vehicle> Vehicles { get; set; } = [];

    [GraphQLDescription("A vehicle in Star Wars Universe identified by the Star Wars API identifier.")]
    public Vehicle VehicleById { get; set; } = default!;
}
