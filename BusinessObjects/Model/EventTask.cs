using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class EventTask
    {
        public EventTask()
        {
            PlayerHistories = new HashSet<PlayerHistory>();
        }

        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid EventId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual Event Event { get; set; }
        public virtual Task Task { get; set; }
        public virtual ICollection<PlayerHistory> PlayerHistories { get; set; }
    }
}
