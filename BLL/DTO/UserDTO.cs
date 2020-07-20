using Microsoft.AspNetCore.Identity;
using System;

namespace BAL.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }
        public int? Finished { get; set; }
        public int? Failed { get; set; }
        public int? Expired { get; set; }
        public double Rating { get; set; }
    }
}
