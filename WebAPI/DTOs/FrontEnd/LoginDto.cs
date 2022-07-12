using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.FrontEnd
{
    public class LoginReplyDto : EmployeeDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }

    public class LoginDto {
        public int Id { get; set; }
        public string Password { get; set; } = null!;
    }
}