using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entites
{
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [ForeignKey("FK_Taskss_ToUser")]
        public int? UserId { get; set; }
        [ForeignKey("FK_Taskss_ToProject")]
        public int? ProjectId { get; set; }
        [ForeignKey("FK_Taskss_ToTaskss")]
        public int? ParentTaskId { get; set; }

        public string TaskName { get; set; }

        public string Disription { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EstimateDate { get; set; }

        public int? RatingPoints { get; set; }

        public Task ParentTask { get; set; }
        public Project Project { get; set; }

        public User User { get; set; }
        public ICollection<Task> ChildTasks { get; set; }

        public Task()
        {
            this.ChildTasks = new List<Task>();
        }
    }
}
