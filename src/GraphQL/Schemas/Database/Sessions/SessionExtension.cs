namespace GraphQL.Schemas.Database.Sessions;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.SessionAttendees;
using GraphQL.Repositories.Database.SessionSpeakers;
using GraphQL.Repositories.Database.Tracks;
using GraphQL.Schemas.Database.Attendees;
using GraphQL.Schemas.Database.Speakers;
using GraphQL.Schemas.Database.Tracks;

[ExtendObjectType(typeof(SessionSchema))]
public class SessionExtension(
    [Service] ISessionAttendeeRepository sessionAttendees,
    [Service] ISessionSpeakerRepository sessionSpeakers,
    [Service] ITrackRepository tracks)
{
    public async Task<TrackSchema> GetTrackAsync(
        [Parent] SessionSchema parent,
        CancellationToken ctx)
    {
        var response = await tracks.GetByIdAsync(parent.TrackId, ctx)
            ?? throw new TrackNotFoundException();

        return TrackSchema.MapFrom(response);
    }

    public async Task<IEnumerable<SpeakerSchema>> GetSpeakersAsync(
        [Parent] SessionSchema parent,
        CancellationToken ctx)
    {
        var response = await sessionSpeakers.GetSpeakersBySessionAsync(parent.Id, ctx);

        return response.Select(SpeakerSchema.MapFrom);
    }

    public async Task<IEnumerable<AttendeeSchema>> GetAttendeesAsync(
        [Parent] SessionSchema parent,
        CancellationToken ctx)
    {
        var response = await sessionAttendees.GetAttendeesBySessionAsync(parent.Id, ctx);

        return response.Select(AttendeeSchema.MapFrom);
    }
}
