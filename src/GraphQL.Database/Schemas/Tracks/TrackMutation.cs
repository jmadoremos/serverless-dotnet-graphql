namespace GraphQL.Database.Schemas.Tracks;

using GraphQL.Database.Exceptions;
using GraphQL.Database.Repositories.Tracks;

[ExtendObjectType("Mutation")]
public class TrackMutation([Service] ITrackRepository tracks)
{
    [GraphQLDescription("Adds a track resource.")]
    public async Task<Track> AddTrackAsync(
        AddTrackInput input,
        CancellationToken ctx)
    {
        var attendee = TrackModelInput.MapFrom(input);

        var id = await tracks.CreateTrackAsync(attendee, ctx);

        var entity = await tracks.GetTrackByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        return Track.MapFrom(entity);
    }

    [GraphQLDescription("Updates a track resource.")]
    public async Task<Track> UpdateTrackAsync(
        [ID(nameof(Track))] int id,
        UpdateTrackInput input,
        CancellationToken ctx)
    {
        var entity = await tracks.GetTrackByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        var attendee = TrackModelInput.MapFrom(entity, input);

        await tracks.UpdateTrackAsync(id, attendee, ctx);

        entity = await tracks.GetTrackByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        return Track.MapFrom(entity);
    }

    [GraphQLDescription("Deletes a track resource.")]
    public async Task<Track> DeleteTrackAsync(
        [ID(nameof(Track))] int id,
        CancellationToken ctx)
    {
        var entity = await tracks.GetTrackByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        await tracks.DeleteTrackAsync(id, ctx);

        return Track.MapFrom(entity);
    }
}
