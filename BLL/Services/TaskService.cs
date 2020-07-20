using AutoMapper;
using BAL.DTO;
using BAL.Interfaces;
using DAL.Entites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Services
{
    public class TaskService : ITaskService
    {
        IUnitOfWork Database { get; set; }

        public TaskService(IUnitOfWork db)
        {
            this.Database = db;
        }

        public void AddTask(TaskDTO taskdto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskDTO, Task>()).CreateMapper();

            Database.Tasks.Create(mapper.Map<TaskDTO, Task>(taskdto));
            Database.Save();
        }

        public void Save() 
        {
            Database.Save();
        }

        public bool Delete(int? id)
        {
            Task task = Database.Tasks.Get(id.Value);
            if (task != null)
            {
                Database.Tasks.Delete(id.Value);
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

        public TaskDTO GetTask(int? id)
        {
            Task task = Database.Tasks.Get(id.Value);
            if (task != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>()).CreateMapper();
                return mapper.Map<Task, TaskDTO>(task);
            }
            else
            {
                throw new Exception("Not Found!");
            }
        }

        public List<TaskDTO> GetTasks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Task>, List<TaskDTO>>(Database.Tasks.GetAll());
        }

        public void UpdateTask(int? id, TaskDTO task)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskDTO, Task>()).CreateMapper();
            this.Database.Tasks.Update(mapper.Map<TaskDTO, Task>(task));
            this.Database.Save();
        }
    }
}
