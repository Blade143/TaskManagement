using System.Collections.Generic;

namespace DAL.Entites
{
    public class Project
    {
        public int ProjectId {get;set;}
        public string title { get; set; }

        public string status { get; set; }

        public string image { get; set; }
        public string discription { get; set; }

        public ICollection<Task> Tasks { get; set; }

        public ICollection<ProjectUserRole> projectUserRoles { get; set; }

        public Project() 
        {
            this.projectUserRoles = new List<ProjectUserRole>();
            this.Tasks = new List<Task>();
        }
    }
}
