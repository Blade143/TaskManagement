using BAL.DTO;
using System.Collections.Generic;

namespace BAL.Interfaces
{
    public interface IProjectService
    {
        void AddProject(ProjectDTO project);
        ProjectDTO GetProject(int? id);

        ProjectDTO GetProjectByTitle(string title); 
        List<ProjectDTO> GetProjects();
        void UpdateProject(int? id, ProjectDTO project);
        bool Delete(int? id);
        void Dispose();
    }
}
