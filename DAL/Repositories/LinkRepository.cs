using System.Collections.Generic;
using DAL.Interfaces;
using DAL.Entites;
using DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class LinkRepository : IRepository<ProjectUserRole>
    {
        private CommContext db;

        public LinkRepository(CommContext ctx)
        {
            this.db = ctx;
        }

        public void Create(ProjectUserRole link)
        {
            db.ProjectUserRole.Add(link);
        }

        public void Delete(int id)
        {
            ProjectUserRole link = db.ProjectUserRole.Find(id);
            if (link != null)
                db.ProjectUserRole.Remove(link);
        }

        public ProjectUserRole Get(int id)
        {
            return db.ProjectUserRole.Find(id);
        }

        public IEnumerable<ProjectUserRole> GetAll()
        {
            return db.ProjectUserRole;
        }

        public void Update(ProjectUserRole link)
        {
            db.Entry(link).State = EntityState.Modified;
        }
    }
}
