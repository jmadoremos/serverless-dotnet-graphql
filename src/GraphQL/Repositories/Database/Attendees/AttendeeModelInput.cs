namespace GraphQL.Repositories.Database.Attendees;

using GraphQL.Schemas.Database.Attendees;
using System.ComponentModel.DataAnnotations;

public class AttendeeModelInput
{
    [Required]
    [StringLength(200)]
    public string FirstName { get; set; } = default!;

    [Required]
    [StringLength(200)]
    public string LastName { get; set; } = default!;

    [Required]
    [StringLength(200)]
    public string UserName { get; set; } = default!;

    [StringLength(256)]
    public string? EmailAddress { get; set; } = default!;

    public static AttendeeModelInput MapFrom(AddAttendeeInput s) => new()
    {
        FirstName = s.Firstname,
        LastName = s.Lastname,
        UserName = s.Username,
        EmailAddress = s.Email
    };

    public static AttendeeModelInput MapFrom(AttendeeModel o, UpdateAttendeeInput s) => new()
    {
        FirstName = s.Firstname ?? o.FirstName,
        LastName = s.Lastname ?? o.LastName,
        UserName = o.UserName,
        EmailAddress = s.Email ?? o.EmailAddress
    };
}
