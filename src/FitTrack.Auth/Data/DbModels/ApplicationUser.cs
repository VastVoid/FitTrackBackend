using System.ComponentModel.DataAnnotations.Schema;

namespace FitTrack.Auth.Data.DbModels;

[Table("Users")]
public class ApplicationUser
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
}