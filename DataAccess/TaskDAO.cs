using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TaskDAO
    {
        private static TaskDAO instance = null;
        private static readonly object instanceLock = new object();
        private TaskDAO() { }

        public static TaskDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TaskDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<BusinessObjects.Model.Task> GetTasks()
        {
            List<BusinessObjects.Model.Task> tasks = new List<BusinessObjects.Model.Task>();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    tasks = context.Tasks.Include(l=>l.Location).Include(m=>m.Major).Include(n=>n.Npc).ToList();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error at GetAllTasks: "+ex);
            }
            return tasks;
        }

        public BusinessObjects.Model.Task GetTaskByID(Guid id)
        {
            BusinessObjects.Model.Task task = new BusinessObjects.Model.Task();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    task = context.Tasks.SingleOrDefault(a => a.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetTaskByID: " + ex.Message);
            }
            return task;
        }

        public BusinessObjects.Model.Task CreateNewTask(BusinessObjects.Model.Task task)
        {
            try
            {
                var context = new FPTHCMAdventuresDBContext();
                task.Id = Guid.NewGuid();
                context.Tasks.Add(task);
            }
            catch(Exception ex)
            {
                throw new Exception("Error at CreateNewTask: "+ ex.Message);
            }
            return task;
        }

        public BusinessObjects.Model.Task UpdateTask(BusinessObjects.Model.Task task)
        {
            try
            {
                var context = new FPTHCMAdventuresDBContext();
                task.Id = Guid.NewGuid();
                context.Tasks.Update(task);
            }
            catch (Exception ex)
            {
                throw new Exception("Error at UpdateTask: " + ex.Message);
            }
            return task;

        }
    }
}
