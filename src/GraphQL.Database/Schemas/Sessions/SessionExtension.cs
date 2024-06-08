namespace GraphQL.Database.Schemas.Sessions;

using GraphQL.Database.Repositories.SessionAttendees;
using GraphQL.Database.Repositories.SessionSpeakers;
using GraphQL.Database.Repositories.Tracks;
using GraphQL.Database.Schemas.Attendees;
using GraphQL.Database.Schemas.Speakers;
using GraphQL.Database.Schemas.Tracks;

[ExtendObjectType(typeof(Session))]
public class SessionExtension(
    [Service] ISessionAttendeeRepository sessionAttendees,
    [Service] ISessionSpeakerRepository sessionSpeakers,
    [Service] ITrackRepository tracks)
{
    [BindMember(nameof(Session.TrackId))]
    [GraphQLDescription("The track of this session.")]
    public async Task<Track?> GetTrackAsync(
        [Parent] Session parent,
        CancellationToken ctx)
    {
        var response = await tracks.GetTrackByIdAsync(parent.TrackId, ctx);

        if (response is null)
        {
            return null;
        }

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
