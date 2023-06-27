using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Major
    {
        public Major()
        {
            Tasks = new HashSet<Task>();
        }

        public Guid Id { get; set; }
        public Guid? QuestionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
