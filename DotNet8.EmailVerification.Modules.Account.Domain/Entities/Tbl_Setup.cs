using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet8.EmailVerification.Modules.Account.Domain.Entities;

[Table("Tbl_Setup")]
public class Tbl_Setup
{
    [Key]
    public string SetupId { get; set; }
    public string UserId { get; set; }
    public string SetupCode { get; set; }
    public DateTime CreatedDate { get; set; }
}
