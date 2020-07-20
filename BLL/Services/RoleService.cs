using AutoMapper;
using BAL.DTO;
using BAL.Interfaces;
using DAL.Entites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace BAL.Services
{
    public class RoleService : IRoleService
    {
        IUnitOfWork Database { get; set; }

        public RoleService(IUnitOfWork db)
        {
            this.Database = db;
        }

        public void AddRole(RoleDTO roledto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, Role>()).CreateMapper();

            Database.Roles.Create(mapper.Map<RoleDTO, Role>(roledto));
            Database.Save();
        }

        public bool Delete(int? id)
        {
            Role role = Database.Roles.Get(id.Value);
            if (role != null)
            {
                Database.Roles.Delete(id.Value);
                Database.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public RoleDTO GetRole(int? id)
        {
            Role role = Database.Roles.Get(id.Value);
            if (role != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
                return mapper.Map<Role, RoleDTO>(role);
            }
            else
            {
                throw new Exception("Not Found!");
            }
        }

        public List<RoleDTO> GetRoles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Role>, List<RoleDTO>>(Database.Roles.GetAll());
        }

        public void UpdateRole(int? id, RoleDTO role)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, Role>()).CreateMapper();
            this.Database.Roles.Update(mapper.Map<RoleDTO, Role>(role));
            this.Database.Save();
        }
    }
}
