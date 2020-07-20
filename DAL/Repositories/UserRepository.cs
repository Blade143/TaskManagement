using System.Collections.Generic;
using DAL.Interfaces;
using DAL.Entites;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private CommContext db;

        public UserRepository(CommContext ctx)
        {
            this.db = ctx;
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public User GetByLogin(string login)
        {
            return db.Users.Where(x => x.Login == login).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User GetNoTrack(int id) 
        {
            return db.Users.AsNoTracking().Where(x => x.UserID == id).FirstOrDefault();
        }
        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }
    }
}
