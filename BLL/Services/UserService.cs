using AutoMapper;
using BAL.DTO;
using BAL.Interfaces;
using DAL.Entites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork db)
        {
            this.Database = db;
        }

        public void AddUser(UserDTO userdto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()).CreateMapper();

            Database.Users.Create(mapper.Map<UserDTO, User>(userdto));
            Database.Save();
        }

        public bool Delete(int? id)
        {
            User user = Database.Users.Get(id.Value);
            if (user != null)
            {
                Database.Users.Delete(id.Value);
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

        public UserDTO GetUser(int? id)
        {
            User user = Database.Users.Get(id.Value);
            if (user != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
                return mapper.Map<User, UserDTO>(user);
            }
            else
            {
                throw new Exception("Not Found!");
            }
        }

        public List<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }

        public void UpdateUser(int? id, UserDTO user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()).CreateMapper();
            this.Database.Users.Update(mapper.Map<UserDTO, User>(user));
            this.Database.Save();
        }

        public UserDTO GetUserByLogin(string login)
        {
            User user = Database.Users.GetByLogin(login);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<User, UserDTO>(user);
        }

        UserDTO IUserService.GetUserNoTraking(int? id)
        {
            User user = Database.Users.GetNoTrack(id.Value);
            if (user != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
                return mapper.Map<User, UserDTO>(user);
            }
            else
            {
                throw new Exception("Not Found!");
            }
        }
    }
}