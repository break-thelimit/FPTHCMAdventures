using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetEvents();
        Event UpdateEvent(Event event1);
        Event CreateEvent(Event event1);
    }
}
