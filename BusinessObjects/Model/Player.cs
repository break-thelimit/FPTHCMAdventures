using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Player
    {
        public Player()
        {
            ExchangeHistories = new HashSet<ExchangeHistory>();
            Inventories = new HashSet<Inventory>();
            PlayHistories = new HashSet<PlayHistory>();
            Ranks = new HashSet<Rank>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Nickname { get; set; }
        public DateTime CreatedAt { get; set; }
        public double TotalPoint { get; set; }
        public double TotalTime { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ExchangeHistory> ExchangeHistories { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<PlayHistory> PlayHistories { get; set; }
        public virtual ICollection<Rank> Ranks { get; set; }
    }
}
