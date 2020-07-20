using System;
using System.Collections.Generic;

namespace DAL.Entites
{
    public class User
    {
        public int UserID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }
        public int? Finished { get; set; }
        public int? Failed { get; set; }
        public int? Expired { get; set; }
        public double Rating { get; set; }

        public ICollection<Task> Tasks { get; set; }

        public ICollection<ProjectUserRole> projectUserRoles { get; set; }

        public User() 
        {
            this.projectUserRoles = new List<ProjectUserRole>();
            this.Tasks = new List<Task>();
        }
    }
}
