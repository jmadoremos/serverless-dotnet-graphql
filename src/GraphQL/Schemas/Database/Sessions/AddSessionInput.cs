namespace GraphQL.Schemas.Database.Sessions;

[GraphQLDescription("A session resource of tracks.")]
public class AddSessionInput
{
    [GraphQLDescription("The title of this session.")]
    public string Title { get; set; } = default!;

    [GraphQLDescription("The abstract of this session.")]
    public string? Abstract { get; set; } = default!;

    [GraphQLDescription("The start time of this session.")]
    public DateTimeOffset? StartTime { get; set; } = default!;

    [GraphQLDescription("The end time of this session.")]
    public DateTimeOffset? EndTime { get; set; } = default!;

    [GraphQLDescription("The track Id of this session.")]
    public int TrackId { get; set; } = default!;
}
