using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Npc
    {
        public Npc()
        {
            Tasks = new HashSet<Task>();
        }

        public Guid Id { get; set; }
        public string NpcName { get; set; }
        public string Introduce { get; set; }
        public Guid? QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
