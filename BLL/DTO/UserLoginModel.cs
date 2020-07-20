using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class UserLoginModel : IdentityUser
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
