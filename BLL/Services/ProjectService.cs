using BAL.DTO;
using BAL.Interfaces;
using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DAL.Entites;
using AutoMapper;
using System.Linq;

namespace BAL.Services
{
    public class ProjectService : IProjectService
    {
        IUnitOfWork Database { get; set; }

        public ProjectService(IUnitOfWork db)
        {
            this.Database = db;
        }

        public void AddProject(ProjectDTO projectdto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>()).CreateMapper();

            Database.Projects.Create(mapper.Map<ProjectDTO, Project>(projectdto));
            Database.Save();
        }

        public bool Delete(int? id)
        {
            Project project = Database.Projects.Get(id.Value);
            if (project != null)
            {
                Database.Projects.Delete(id.Value);
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

        public ProjectDTO GetProject(int? id)
        {
            Project project = Database.Projects.Get(id.Value);
            if (project != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
                return mapper.Map<Project, ProjectDTO>(project);   
            }
            else
            {
                throw new Exception("Not Found!");
            }
        }

        public List<ProjectDTO> GetProjects()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Project>, List<ProjectDTO>>(Database.Projects.GetAll());
        }

        public void UpdateProject(int? id, ProjectDTO project)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>()).CreateMapper();
            this.Database.Projects.Update(mapper.Map<ProjectDTO, Project>(project));
            this.Database.Save();
        }

        public ProjectDTO GetProjectByTitle(string title)
        {
            Project project = Database.Projects.GetAll().Where(x => x.title == title).Select(x => x).FirstOrDefault();
            if (project != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
                return mapper.Map<Project, ProjectDTO>(project);
            }
            else
            {
                throw new Exception("Not Found!");
            }
        }
    }
}
