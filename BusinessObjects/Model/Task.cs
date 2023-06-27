using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Task
    {
        public Task()
        {
            EventTasks = new HashSet<EventTask>();
            PlayHistories = new HashSet<PlayHistory>();
            TaskItems = new HashSet<TaskItem>();
        }

        public Guid Id { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? MajorId { get; set; }
        public Guid? NpcId { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? IsRequireitem { get; set; }
        public int? TimeOutAmount { get; set; }
        public string ActivityName { get; set; }
        public int? Point { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public virtual Location Location { get; set; }
        public virtual Major Major { get; set; }
        public virtual Npc Npc { get; set; }
        public virtual ICollection<EventTask> EventTasks { get; set; }
        public virtual ICollection<PlayHistory> PlayHistories { get; set; }
        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}
