using System.Collections.Generic;
using DAL.Interfaces;
using DAL.Entites;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private CommContext db;

        public TaskRepository(CommContext ctx)
        {
            this.db = ctx;
        }

        public void Create(Task task)
        {
            db.Taskss.Add(task);
        }

        public void Delete(int id)
        {
            Task task = db.Taskss.Find(id);
            if (task != null)
                db.Taskss.Remove(task);
        }

        public Task Get(int id)
        {
            return db.Taskss.AsNoTracking().ToList().Where(x=>x.TaskId == id).Select(x=>x).FirstOrDefault();
        }

        public IEnumerable<Task> GetAll()
        {
            return db.Taskss;
        }

        public void Update(Task task)
        {
            db.Update(task);
        }
    }
}
