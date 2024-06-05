namespace GraphQL.Database.Schemas.Tracks;

using GraphQL.Database.Errors;
using GraphQL.Database.Repositories.Tracks;

[ExtendObjectType("Mutation")]
public class TrackMutation([Service] ITrackRepository tracks)
{
    [GraphQLDescription("Adds a track resource.")]
    public async Task<AddRemoveTrackPayload> AddTrackAsync(
        AddTrackInput input,
        CancellationToken ctx)
    {
        var track = TrackModelInput.MapFrom(input);

        var id = await tracks.CreateTrackAsync(track, ctx);

        if (id.IsError)
        {
            return AddRemoveTrackPayload.MapFrom(id.Errors);
        }

        return AddRemoveTrackPayload.MapFrom(id.Value, track);
    }

    [GraphQLDescription("Updates a track resource.")]
    public async Task<UpdateTrackPayload> UpdateTrackAsync(
        [ID(nameof(Track))] int id,
        UpdateTrackInput input,
        CancellationToken ctx)
    {
        var entity = await tracks.GetTrackByIdAsync(id, ctx);

        if (entity is null)
        {
            return UpdateTrackPayload.MapFrom(TrackError.NotFound(id));
        }

        var track = TrackModelInput.MapFrom(entity, input);

        var update = await tracks.UpdateTrackAsync(id, track, ctx);

        if (update.IsError)
        {
            return UpdateTrackPayload.MapFrom(entity, update.Errors);
        }

        return UpdateTrackPayload.MapFrom(id, entity, track);
    }

    [GraphQLDescription("Deletes a track resource.")]
    public async Task<AddRemoveTrackPayload> RemoveTrackAsync(
        [ID(nameof(Track))] int id,
        CancellationToken ctx)
    {
        var entity = await tracks.GetTrackByIdAsync(id, ctx);

        if (entity is null)
        {
            return AddRemoveTrackPayload.MapFrom(AttendeeError.NotFound(id));
        }

        var delete = await tracks.DeleteTrackAsync(id, ctx);

        if (delete.IsError)
        {
            return AddRemoveTrackPayload.MapFrom(delete.Errors);
        }

        return AddRemoveTrackPayload.MapFrom(entity);
    }
}
