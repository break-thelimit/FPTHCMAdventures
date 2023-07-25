using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Event
    {
        public Event()
        {
            EventTasks = new HashSet<EventTask>();
            Ranks = new HashSet<Rank>();
            SchoolEvents = new HashSet<SchoolEvent>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }

        public virtual ICollection<EventTask> EventTasks { get; set; }
        public virtual ICollection<Rank> Ranks { get; set; }
        public virtual ICollection<SchoolEvent> SchoolEvents { get; set; }
    }
}
