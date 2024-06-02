namespace GraphQL.Database.Exceptions;

using GraphQL.Database.Repositories.SessionSpeakers;

public class SessionSpeakerNotFoundException : GraphQLException
{
    public SessionSpeakerNotFoundException() : base("Session speaker relation not found") =>
        this.Attributes =
        [
            "input",
            nameof(SessionSpeakerModel.SessionId),
            nameof(SessionSpeakerModel.SpeakerId),
        ];
}
