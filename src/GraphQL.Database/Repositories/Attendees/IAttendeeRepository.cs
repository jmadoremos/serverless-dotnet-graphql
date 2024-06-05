namespace GraphQL.Database.Repositories.Attendees;

using ErrorOr;

public interface IAttendeeRepository
{
    Task<IQueryable<AttendeeModel>> GetAllAttendeesAsync(CancellationToken ctx);

    Task<AttendeeModel?> GetAttendeeByIdAsync(int id, CancellationToken ctx);

    Task<IQueryable<AttendeeModel>> GetAttendeesByIdsAsync(IEnumerable<int> ids, CancellationToken ctx);

    Task<AttendeeModel?> GetAttendeeByUserNameAsync(string userName, CancellationToken ctx);

    Task<ErrorOr<int>> CreateAttendeeAsync(AttendeeModelInput input, CancellationToken ctx);

    Task<ErrorOr<Updated>> UpdateAttendeeAsync(int id, AttendeeModelInput input, CancellationToken ctx);

    Task<ErrorOr<Deleted>> DeleteAttendeeAsync(int id, CancellationToken ctx);
}
