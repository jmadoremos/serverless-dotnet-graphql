namespace GraphQL.Schemas.Database.Tracks;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Tracks;

[ExtendObjectType("Mutation")]
public class TrackMutation([Service] ITrackRepository tracks)
{
    [GraphQLDescription("Adds a track resource.")]
    public async Task<TrackSchema> AddTrackAsync(
        AddTrackSchema input,
        CancellationToken ctx)
    {
        var attendee = TrackInput.MapFrom(input);

        var id = await tracks.CreateAsync(attendee, ctx);

        var entity = await tracks.GetByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        return TrackSchema.MapFrom(entity);
    }

    [GraphQLDescription("Updates a track resource.")]
    public async Task<TrackSchema> UpdateTrackAsync(
        [GraphQLType(typeof(IdType))] int id,
        UpdateTrackSchema input,
        CancellationToken ctx)
    {
        var entity = await tracks.GetByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        var attendee = TrackInput.MapFrom(entity, input);

        await tracks.UpdateAsync(id, attendee, ctx);

        entity = await tracks.GetByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        return TrackSchema.MapFrom(entity);
    }

    [GraphQLDescription("Deletes a track resource.")]
    public async Task<TrackSchema> DeleteTrackAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        var entity = await tracks.GetByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        await tracks.DeleteAsync(id, ctx);

        return TrackSchema.MapFrom(entity);
    }
}
