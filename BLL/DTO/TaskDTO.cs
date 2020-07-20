using System;

namespace BAL.DTO
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public int? UserId { get; set; }

        public int? ProjectId { get; set; }

        public int? ParentTaskId { get; set; }

        public string TaskName { get; set; }

        public string Disription { get; set; }
        public string Status { get; set; }
        public int? RatingPoints { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EstimateDate { get; set; }
    }
}
