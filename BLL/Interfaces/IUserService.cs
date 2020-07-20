using BAL.DTO;
using System.Collections.Generic;

namespace BAL.Interfaces
{
    public interface IUserService
    {
        void AddUser(UserDTO user);
        UserDTO GetUser(int? id);
        UserDTO GetUserByLogin(string login);
        UserDTO GetUserNoTraking(int? id);
        List<UserDTO> GetUsers();
        void UpdateUser(int? id, UserDTO user);
        bool Delete(int? id);
        void Dispose();
    }
}
