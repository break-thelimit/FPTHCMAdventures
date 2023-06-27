using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Majors = new HashSet<Major>();
        }

        public Guid Id { get; set; }
        public Guid? NpcId { get; set; }
        public string QuestionName { get; set; }

        public virtual Npc Npc { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Major> Majors { get; set; }
    }
}
