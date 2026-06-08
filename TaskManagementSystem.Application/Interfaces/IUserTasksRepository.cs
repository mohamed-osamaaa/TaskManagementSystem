using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface IUserTasksRepository
    {
        Task<IEnumerable<TaskItem>> GetTasksBySubscriptionLevelAsync(SubscriptionType level);
        Task<TaskItem?> GetTaskByIdAsync(Guid taskId);
        Task UpdateTaskStatusAsync(TaskItem task);
    }
}
