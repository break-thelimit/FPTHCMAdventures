using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.TaskItemDto
{
    public abstract class BaseTaskItemDto
    {
        public Guid? TaskId { get; set; }
        public Guid? ItemId { get; set; }
    }
}
