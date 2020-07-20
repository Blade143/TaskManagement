using System.Collections.Generic;

namespace DAL.Entites
{
    public class Role
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }
        public int? Priority { get; set; }

        public ICollection<ProjectUserRole> projectUserRoles { get; set; }

        public Role() 
        {
            this.projectUserRoles = new List<ProjectUserRole>();
        }
    }
}
