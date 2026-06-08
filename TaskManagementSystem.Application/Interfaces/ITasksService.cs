using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common;
using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface ITasksService
    {
        Task<ApiResponse<IEnumerable<TaskItemDto>>> GetAllTasksAsync();
        Task<ApiResponse<TaskItemDto>> GetTaskByIdAsync(Guid id);
        Task<ApiResponse<TaskItemDto>> CreateTaskAsync(TaskItemDto taskDto);
        Task<ApiResponse<TaskItemDto>> UpdateTaskAsync(Guid id, TaskItemDto taskDto);
        Task<ApiResponse<TaskItemDto>> DeleteTaskAsync(Guid id);
    }
}
