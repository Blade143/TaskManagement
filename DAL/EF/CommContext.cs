using DAL.Entites;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class CommContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUserRole> ProjectUserRole { get; set; }
        public DbSet<Task> Taskss { get; set; }
        public DbSet<Role> Roles { get; set; }


        public CommContext(DbContextOptions<CommContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Task>()
                .HasMany(oj => oj.ChildTasks)
                .WithOne(j => j.ParentTask)
                .HasForeignKey(j => j.TaskId);
        }

    }
}
