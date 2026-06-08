using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common;
using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface IUserTasksService
    {
        Task<ApiResponse<IEnumerable<TaskItemDto>>> GetMyTasksAsync(Guid userId);
        Task<ApiResponse<TaskItemDto>> UpdateTaskStatusAsync(Guid userId, Guid taskId, UpdateTaskStatusDto dto);
    }
}
