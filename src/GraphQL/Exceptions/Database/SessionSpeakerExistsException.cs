namespace GraphQL.Exceptions.Database;

using GraphQL.Repositories.Database.SessionSpeakers;

public class SessionSpeakerExistsException : GraphQLException
{
    public SessionSpeakerExistsException() : base("Session speaker relation exists") =>
        this.Attributes =
        [
            "input",
            nameof(SessionSpeaker.SessionId),
            nameof(SessionSpeaker.SpeakerId),
        ];
}
