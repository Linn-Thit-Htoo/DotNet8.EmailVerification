using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.EmailVerification.Modules.Account.Domain.Entities
{
    [Table("Tbl_User")]
    public class Tbl_User
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
