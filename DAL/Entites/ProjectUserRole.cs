using System.ComponentModel.DataAnnotations;

namespace DAL.Entites
{
    public class ProjectUserRole
    {
        [Key]
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? ProjectId { get; set; }
        public int? UserId { get; set; }
    }
}
