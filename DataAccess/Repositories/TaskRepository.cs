using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public BusinessObjects.Model.Task CreateNewTask(BusinessObjects.Model.Task task) => TaskDAO.Instance.CreateNewTask(task);

        public BusinessObjects.Model.Task GetTaskByID(Guid id) => TaskDAO.Instance.GetTaskByID(id);
        public IEnumerable<BusinessObjects.Model.Task> GetTasks() => TaskDAO.Instance.GetTasks();

        public BusinessObjects.Model.Task CreateTask(BusinessObjects.Model.Task task) => TaskDAO.Instance.UpdateTask(task);

    }
}
