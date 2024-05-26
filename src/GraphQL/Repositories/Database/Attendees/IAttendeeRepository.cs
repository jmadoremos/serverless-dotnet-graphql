namespace GraphQL.Repositories.Database.Attendees;

public interface IAttendeeRepository
{
    Task<IQueryable<Attendee>> GetAllAsync(CancellationToken ctx);

    Task<Attendee> GetByIdAsync(int id, CancellationToken ctx);

    Task<Attendee> GetByUserNameAsync(string userName, CancellationToken ctx);

    Task<int> CreateAsync(AttendeeInput input, CancellationToken ctx);

    Task UpdateAsync(int id, AttendeeInput input, CancellationToken ctx);

    Task DeleteAsync(int id, CancellationToken ctx);
}
