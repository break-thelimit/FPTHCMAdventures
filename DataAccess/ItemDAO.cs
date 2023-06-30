using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ItemDAO
    {
        private static ItemDAO instance = null;
        private static readonly object instanceLock = new object();
        private ItemDAO() { }


        public static ItemDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ItemDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<Item> GetItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    items = context.Items.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetAllEvents: " + ex.Message);
            }
            return items;
        }

        public Item GetItemByID(Guid id)
        {
            Item item = new Item();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    item = context.Items.SingleOrDefault(a => a.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetItemByID: " + ex.Message);
            }
            return item;
        }

        public IEnumerable<TaskItem> GetItemsByTask(Guid id)
        {
            List<Item> items = new List<Item>();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    items = (List<Item>)context.TaskItems.Where(a => a.TaskId.Equals(id));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetAllEvents: " + ex.Message);
            }
            return (IEnumerable<TaskItem>)items;
        }

        public Item UpdateItem(Item item)
        {
            try
            {
                var context = new FPTHCMAdventuresDBContext();
                context.Items.Update(item);
            }
            catch (Exception ex)
            {
                throw new Exception("Error at UpdateItem: " + ex.Message);
            }
            return item;
        }

        public Item CreateItem(Item item)
        {
            try
            {
                var context = new FPTHCMAdventuresDBContext();
                context.Items.Add(item);
            }
            catch (Exception ex)
            {
                throw new Exception("Error at CreateItem: " + ex.Message);
            }
            return item;
        }

    }
}
