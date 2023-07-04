using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.TaskItemDto
{
    public class UpdateTaskItemDto : BaseTaskItemDto, IBaseDto
    {
        public Guid Id { get; set; }

    }
}
