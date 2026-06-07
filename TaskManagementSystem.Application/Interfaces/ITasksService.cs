using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common;
using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface ITasksService
    {
        Task<ApiResponse<IEnumerable<TaskItemDto>>> GetAllTasksAsync();
    }
}
