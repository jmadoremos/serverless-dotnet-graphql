namespace GraphQL.Repositories.Database.Attendees;

public interface IAttendeeRepository
{
    Task<IQueryable<AttendeeModel>> GetAllAttendeesAsync(CancellationToken ctx);

    Task<AttendeeModel?> GetAttendeeByIdAsync(int id, CancellationToken ctx);

    Task<IQueryable<AttendeeModel>> GetAttendeesByIdsAsync(IEnumerable<int> ids, CancellationToken ctx);

    Task<AttendeeModel?> GetAttendeeByUserNameAsync(string userName, CancellationToken ctx);

    Task<int> CreateAttendeeAsync(AttendeeModelInput input, CancellationToken ctx);

    Task UpdateAttendeeAsync(int id, AttendeeModelInput input, CancellationToken ctx);

    Task DeleteAttendeeAsync(int id, CancellationToken ctx);
}
