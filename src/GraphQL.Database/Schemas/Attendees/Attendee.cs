namespace GraphQL.Database.Schemas.Attendees;

using GraphQL.Database.DataLoaders;
using GraphQL.Database.Repositories.Attendees;
using GraphQL.Database.Schemas.Sessions;

[Node]
[GraphQLDescription("An attendee resource of sessions.")]
public class Attendee
{
    [ID]
    [GraphQLDescription("The unique identifier of this attendee.")]
    public int Id { get; set; } = default!;

    [GraphQLDescription("The firstnames of this attendee.")]
    public string Firstname { get; set; } = default!;

    [GraphQLDescription("The lastnames of this attendee.")]
    public string Lastname { get; set; } = default!;

    [GraphQLDescription("The username of this attendee.")]
    public string Username { get; set; } = default!;

    [GraphQLDescription("The email address of this attendee.")]
    public string? Email { get; set; } = default!;

    [GraphQLDescription("The sessions where this attendee will be attending, is attending, or has attended to.")]
    public IEnumerable<Session> Sessions { get; set; } = [];

    [NodeResolver]
    public static Task<Attendee> GetNodeAsync(
        [ID(nameof(Attendee))] int id,
        AttendeeByIdBatchDataLoader dataLoader,
        CancellationToken ctx) => dataLoader.LoadAsync(id, ctx);

    public static Attendee MapFrom(AttendeeModel r) => new()
    {
        Id = r.Id,
        Firstname = r.FirstName,
        Lastname = r.LastName,
        Username = r.UserName,
        Email = r.EmailAddress
    };
}
