using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public LinkRepository Links { get; }
        public ProjectRepository Projects { get; }
        public RoleRepository Roles { get; }
        public TaskRepository Tasks { get; }
        public UserRepository Users { get; }

        public void Save();
        public void Dispose();
    }
}
