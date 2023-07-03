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
            Npcs = new HashSet<Npc>();
        }

        public Guid Id { get; set; }
        public string QuestionName { get; set; }
        public Guid? MajorId { get; set; }

        public virtual Major Major { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Npc> Npcs { get; set; }
    }
}
