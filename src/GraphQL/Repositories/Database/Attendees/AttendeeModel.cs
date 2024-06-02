namespace GraphQL.Repositories.Database.Attendees;

using GraphQL.Repositories.Database.Sessions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Attendees")]
public class AttendeeModel : AttendeeModelInput
{
    [Key]
    public int Id { get; set; }

    public ICollection<SessionModel> Sessions { get; set; } = [];

    public static AttendeeModel MapFrom(AttendeeModelInput i) => new()
    {
        FirstName = i.FirstName,
        LastName = i.LastName,
        UserName = i.UserName,
        EmailAddress = i.EmailAddress
    };
}
