using BAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ILinkService
    {
        void AddLink(ProjectUserRoleDTO link);
        ProjectUserRoleDTO GetLink(int? id);
        List<ProjectUserRoleDTO> GetLinks();
        void UpdateLink(int? id, ProjectUserRoleDTO link);
        bool Delete(int? id);
        void Dispose();
    }
}
