namespace GraphQL.Schemas.Database.Sessions;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.SessionAttendees;
using GraphQL.Repositories.Database.SessionSpeakers;
using GraphQL.Repositories.Database.Tracks;
using GraphQL.Schemas.Database.Attendees;
using GraphQL.Schemas.Database.Speakers;
using GraphQL.Schemas.Database.Tracks;

[ExtendObjectType(typeof(Session))]
public class SessionExtension(
    [Service] ISessionAttendeeRepository sessionAttendees,
    [Service] ISessionSpeakerRepository sessionSpeakers,
    [Service] ITrackRepository tracks)
{
    [BindMember(nameof(Session.TrackId))]
    [GraphQLDescription("The track of this session.")]
    public async Task<Track> GetTrackAsync(
        [Parent] Session parent,
        CancellationToken ctx)
    {
        var response = await tracks.GetTrackByIdAsync(parent.TrackId, ctx)
            ?? throw new TrackNotFoundException();

        return Track.MapFrom(response);
    }

    public async Task<IEnumerable<Speaker>> GetSpeakersAsync(
        [Parent] Session parent,
        CancellationToken ctx)
    {
        var response = await sessionSpeakers.GetSpeakersBySessionAsync(parent.Id, ctx);

        return response.Select(Speaker.MapFrom);
    }

    public async Task<IEnumerable<Attendee>> GetAttendeesAsync(
        [Parent] Session parent,
        CancellationToken ctx)
    {
        var response = await sessionAttendees.GetAttendeesBySessionAsync(parent.Id, ctx);

        return response.Select(Attendee.MapFrom);
    }
}
