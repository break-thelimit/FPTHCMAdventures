using System;
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
            TaskItems = new HashSet<TaskItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public int? Quantity { get; set; }

        public virtual ICollection<ExchangeHistory> ExchangeHistories { get; set; }
        public virtual ICollection<ItemIventory> ItemIventories { get; set; }
        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}
