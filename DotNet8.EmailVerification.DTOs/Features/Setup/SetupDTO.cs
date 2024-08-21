using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.EmailVerification.DTOs.Features.Setup
{
    public class SetupDto
    {
        public string SetupId { get; set; }
        public string UserId { get; set; }
        public string SetupCode { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
