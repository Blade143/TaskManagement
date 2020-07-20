using System.Collections.Generic;
using DAL.Interfaces;
using DAL.Entites;
using DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private CommContext db;

        public ProjectRepository(CommContext ctx)
        {
            this.db = ctx;
        }

        public void Create(Project project)
        {
            db.Projects.Add(project);
        }

        public void Delete(int id)
        {
            Project project = db.Projects.Find(id);
            if (project != null)
                db.Projects.Remove(project);
        }

        public Project Get(int id)
        {
            return db.Projects.Find(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return db.Projects;
        }

        public void Update(Project project)
        {
            db.Entry(project).State = EntityState.Modified;
        }
    }
}
