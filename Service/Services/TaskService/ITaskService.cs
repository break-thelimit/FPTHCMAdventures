using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.TaskDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TaskService
{
    public interface ITaskService
    {
        Task<ServiceResponse<IEnumerable<GetTaskDto>>> GetTask();
        Task<ServiceResponse<TaskDto>> GetTaskById(Guid eventId);

        Task<ServiceResponse<Guid>> CreateNewTask(CreateTaskDto createEventDto);
        Task<ServiceResponse<string>> UpdateTask(Guid id, UpdateTaskDto updateTaskDto);
    }
}
