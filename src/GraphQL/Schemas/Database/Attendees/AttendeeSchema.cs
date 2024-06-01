namespace GraphQL.Schemas.Database.Attendees;

using GraphQL.Repositories.Database.Attendees;
using GraphQL.Schemas.Database.Sessions;

[GraphQLDescription("An attendee resource of sessions.")]
public class AttendeeSchema
{
    [GraphQLType(typeof(IdType))]
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
    public IEnumerable<SessionSchema> Sessions { get; set; } = [];

    public static AttendeeSchema MapFrom(Attendee r) => new()
    {
        Id = r.Id,
        Firstname = r.FirstName,
        Lastname = r.LastName,
        Username = r.UserName,
        Email = r.EmailAddress
    };
}
