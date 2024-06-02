namespace GraphQL.Schemas.Database.Sessions;

using GraphQL.DataLoaders.Database;
using GraphQL.Repositories.Database.Sessions;
using GraphQL.Schemas.Database.Attendees;
using GraphQL.Schemas.Database.Speakers;

[Node]
[GraphQLDescription("A session resource of tracks.")]
public class Session
{
    [ID]
    [GraphQLDescription("The unique identifier of this session.")]
    public int Id { get; set; } = default!;

    [GraphQLDescription("The title of this session.")]
    public string? Title { get; set; } = default!;

    [GraphQLDescription("The abstract of this session.")]
    public string? Abstract { get; set; } = default!;

    [GraphQLDescription("The start time of this session.")]
    public DateTimeOffset? StartTime { get; set; } = default!;

    [GraphQLDescription("The end time of this session.")]
    public DateTimeOffset? EndTime { get; set; } = default!;

    [GraphQLDescription("The duration of this session.")]
    public TimeSpan Duration =>
        this.EndTime?.Subtract(this.StartTime ?? this.EndTime ?? DateTimeOffset.MinValue) ?? TimeSpan.Zero;

    [GraphQLDescription("The track Id of this session.")]
    public int TrackId { get; set; } = default!;

    [GraphQLDescription("A list of speaker resources of this session.")]
    public IEnumerable<Speaker> Speakers { get; set; } = [];

    [GraphQLDescription("A list of attendee resources of this session.")]
    public IEnumerable<Attendee> Attendees { get; set; } = [];

    [NodeResolver]
    public static Task<Session> GetNodeAsync(
        [ID(nameof(Session))] int id,
        [Service] SessionByIdBatchDataLoader dataLoader,
        CancellationToken ctx) => dataLoader.LoadAsync(id, ctx);

    public static Session MapFrom(SessionModel r) => new()
    {
        Id = r.Id,
        Title = r.Title,
        Abstract = r.Abstract,
        StartTime = r.StartTime,
        EndTime = r.EndTime,
        TrackId = r.TrackId,
    };
}
