using BAL.DTO;
using System.Collections.Generic;

namespace BAL.Interfaces
{
    public interface ITaskService
    {
        void AddTask(TaskDTO task);
        TaskDTO GetTask(int? id);
        List<TaskDTO> GetTasks();
        void UpdateTask(int? id, TaskDTO task);
        bool Delete(int? id);
        public void Save();
        void Dispose();
    }
}
