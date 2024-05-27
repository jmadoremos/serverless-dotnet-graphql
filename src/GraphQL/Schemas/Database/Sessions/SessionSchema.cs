namespace GraphQL.Schemas.Database.Sessions;

using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.Tracks;

[GraphQLDescription("A session resource of tracks.")]
public class SessionSchema
{
    [GraphQLType(typeof(IdType))]
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
    public int? TrackId { get; set; } = default!;

    [GraphQLDescription("The track this sessions belongs to.")]
    public Track Track { get; set; } = default!;

    public static SessionSchema MapFrom(Session r) => new()
    {
        Id = r.Id,
        Title = r.Title,
        Abstract = r.Abstract,
        StartTime = r.StartTime,
        EndTime = r.EndTime,
        TrackId = r.TrackId,
    };
}
