using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Rank
    {
        public Rank()
        {
            Events = new HashSet<Event>();
            Gifts = new HashSet<Gift>();
        }

        public Guid Id { get; set; }
        public Guid? PlayerId { get; set; }
        public string RankNumber { get; set; }

        public virtual Player Player { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Gift> Gifts { get; set; }
    }
}
