using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ItemRepository : IITemRepository
    {
        public Item CreateItem(Item item) => ItemDAO.Instance.CreateItem(item);

        public Item GetItemByID(Guid id) => ItemDAO.Instance.GetItemByID(id);

        public IEnumerable<Item> GetItems() => ItemDAO.Instance.GetItems();

        public IEnumerable<TaskItem> GetItemsByTask(Guid id) => ItemDAO.Instance.GetItemsByTask(id);

        public Item UpdateItem(Item item) => ItemDAO.Instance.UpdateItem(item);
    }
}
