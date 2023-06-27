using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EventRepository : IEventRepository
    {
        public Event CreateEvent(Event event1) => EventDAO.Instance.CreateEvent(event1);

        public IEnumerable<Event> GetEvents() => EventDAO.Instance.GetEvents();

        public Event UpdateEvent(Event event1)=> EventDAO.Instance.UpdateEvent(event1);
    }
}
