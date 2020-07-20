using BAL.DTO;
using System.Collections.Generic;

namespace BAL.Interfaces
{
    public interface IRoleService
    {
        void AddRole(RoleDTO role);
        RoleDTO GetRole(int? id);
        List<RoleDTO> GetRoles();
        void UpdateRole(int? id, RoleDTO role);
        bool Delete(int? id);
        void Dispose();
    }
}
