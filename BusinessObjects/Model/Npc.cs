using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Npc
    {
        public Npc()
        {
            Questions = new HashSet<Question>();
            Tasks = new HashSet<Task>();
        }

        public Guid Id { get; set; }
        public string NpcName { get; set; }
        public string Introduce { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
