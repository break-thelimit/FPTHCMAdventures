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
            SchoolEvents = new HashSet<SchoolEvent>();
        }

        public Guid Id { get; set; }
        public Guid? RankId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Status { get; set; }

        public virtual Rank Rank { get; set; }
        public virtual ICollection<EventTask> EventTasks { get; set; }
        public virtual ICollection<SchoolEvent> SchoolEvents { get; set; }
    }
}
