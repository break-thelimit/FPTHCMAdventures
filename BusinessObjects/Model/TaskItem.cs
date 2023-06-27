using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class TaskItem
    {
        public Guid Id { get; set; }
        public Guid? TaskId { get; set; }
        public Guid? ItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Task Task { get; set; }
    }
}
