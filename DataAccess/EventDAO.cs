using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EventDAO
    {
        private static EventDAO instance = null;
        private static readonly object instanceLock = new object();
        private EventDAO() { }


        public static EventDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new EventDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<Event> GetEvents()
        {
            List<Event> events = new List<Event>();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    events = context.Events.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetAllEvents: " + ex.Message);
            }
            return events;
        }

        public Event UpdateEvent(Event event1)
        {
            try
            {
                var context = new FPTHCMAdventuresDBContext();
                context.Events.Update(event1);
            }
            catch (Exception ex)
            {
                throw new Exception("Error at UpdateActivity: " + ex.Message);
            }
            return event1;
        }

        public Event CreateEvent(Event event1)
        {
            try
            {
                var context = new FPTHCMAdventuresDBContext();
                context.Events.Add(event1);
            }
            catch (Exception ex)
            {
                throw new Exception("Error at UpdateActivity: " + ex.Message);
            }
            return event1;
        }


    }
}
