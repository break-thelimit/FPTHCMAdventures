using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IITemRepository
    {
        IEnumerable<Item> GetItems();
        Item GetItemByID(Guid id);
        IEnumerable<TaskItem> GetItemsByTask(Guid id);
        Item UpdateItem(Item item);
        Item CreateItem(Item item);
    }
}
