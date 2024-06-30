using System.ComponentModel.DataAnnotations.Schema;

namespace FitTrack.Auth.Data.DbModels;

[Table("RoleToUserRecords")]
public class ApplicationUserRole
{
    public int UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public int RoleId { get; set; }
    public ApplicationRole? Role { get; set; }
}