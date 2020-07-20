using AutoMapper;
using BAL.DTO;
using BLL.Interfaces;
using DAL.Entites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class LinkService : ILinkService
    {
        IUnitOfWork Database { get; set; }

        public LinkService(IUnitOfWork db)
        {
            this.Database = db;
        }

        public void AddLink(ProjectUserRoleDTO link)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectUserRoleDTO, ProjectUserRole>()).CreateMapper();

            Database.Links.Create(mapper.Map<ProjectUserRoleDTO, ProjectUserRole>(link));
            Database.Save();
        }

        public bool Delete(int? id)
        {
            ProjectUserRole link = Database.Links.Get(id.Value);
            if (link != null)
            {
                Database.Links.Delete(id.Value);
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

        public ProjectUserRoleDTO GetLink(int? id)
        {
            ProjectUserRole link = Database.Links.Get(id.Value);
            if (link != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectUserRole, ProjectUserRoleDTO>()).CreateMapper();
                return mapper.Map<ProjectUserRole, ProjectUserRoleDTO>(link);
            }
            else
            {
                throw new Exception("Not Found!");
            }
        }

        public List<ProjectUserRoleDTO> GetLinks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectUserRole, ProjectUserRoleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ProjectUserRole>, List<ProjectUserRoleDTO>>(Database.Links.GetAll());
        }

        public void UpdateLink(int? id, ProjectUserRoleDTO link)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectUserRoleDTO, ProjectUserRole>()).CreateMapper();
            this.Database.Links.Update(mapper.Map<ProjectUserRoleDTO, ProjectUserRole>(link));
            this.Database.Save();
        }
    }
}
