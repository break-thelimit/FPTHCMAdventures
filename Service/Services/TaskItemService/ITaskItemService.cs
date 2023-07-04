using DataAccess.Dtos.SchoolEventDto;
using DataAccess.Dtos.TaskItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TaskItemService
{
    public interface ITaskItemService
    {
        Task<ServiceResponse<IEnumerable<GetTaskItemDto>>> GetTaskItem();
        Task<ServiceResponse<TaskItemDto>> GetTaskItemById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewTaskItem(CreateTaskItemDto createTaskItemDto);
        Task<ServiceResponse<string>> UpdateTaskItem(Guid id, UpdateTaskItemDto taskItemDto);
    }
}
