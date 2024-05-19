namespace GraphQL.Schemas.StarWars.Films;

using GraphQL.Extensions;
using GraphQL.Repositories.StarWars.Films;

[GraphQLDescription("A film resource is a single film.")]
public class FilmSchema
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public int EpisodeId { get; set; } = default!;

    public string OpeningCrawl { get; set; } = default!;

    public string Director { get; set; } = default!;

    public string Producer { get; set; } = default!;

    public string ReleaseDate { get; set; } = default!;

    public static FilmSchema MapFrom(FilmApiResponse r) => new()
    {
        Id = r.URL.ExtractSwapiId(),
        Title = r.Title,
        EpisodeId = r.EpisodeId,
        OpeningCrawl = r.OpeningCrawl,
        Director = r.Director,
        Producer = r.Producer,
        ReleaseDate = r.ReleaseDate
    };
}
