using System.ComponentModel.DataAnnotations.Schema;

namespace FitTrack.Auth.Data.DbModels;

[Table("Roles")]
public class ApplicationRole
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
}