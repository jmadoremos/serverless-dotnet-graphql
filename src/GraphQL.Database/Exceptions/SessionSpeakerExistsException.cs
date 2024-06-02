namespace GraphQL.Database.Exceptions;

using GraphQL.Database.Repositories.SessionSpeakers;

public class SessionSpeakerExistsException : GraphQLException
{
    public SessionSpeakerExistsException() : base("Session speaker relation exists") =>
        this.Attributes =
        [
            "input",
            nameof(SessionSpeakerModel.SessionId),
            nameof(SessionSpeakerModel.SpeakerId),
        ];
}
