using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<BusinessObjects.Model.Task> GetTasks();
        BusinessObjects.Model.Task GetTaskByID(Guid id);
        BusinessObjects.Model.Task CreateNewTask(BusinessObjects.Model.Task task);
        BusinessObjects.Model.Task CreateTask(BusinessObjects.Model.Task task);
    }
}
