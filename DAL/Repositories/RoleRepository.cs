using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DAL.Entites;
using DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private CommContext db;

        public RoleRepository(CommContext ctx)
        {
            this.db = ctx;
        }

        public void Create(Role role)
        {
            db.Roles.Add(role);
        }

        public void Delete(int id)
        {
            Role role = db.Roles.Find(id);
            if (role != null)
                db.Roles.Remove(role);
        }

        public Role Get(int id)
        {
            return db.Roles.Find(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }

        public void Update(Role role)
        {
            db.Entry(role).State = EntityState.Modified;
        }
    }
}
