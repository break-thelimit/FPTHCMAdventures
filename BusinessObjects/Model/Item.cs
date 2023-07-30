﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Item
    {
        public Item()
        {
            ExchangeHistories = new HashSet<ExchangeHistory>();
            ItemIventories = new HashSet<ItemIventory>();
            Tasks = new HashSet<Task>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool? LimitExchange { get; set; }
        public string Status { get; set; }

        public virtual ICollection<ExchangeHistory> ExchangeHistories { get; set; }
        public virtual ICollection<ItemIventory> ItemIventories { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
