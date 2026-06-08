using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.Services
{
    public class UserTasksService : IUserTasksService
    {
        private readonly IUserTasksRepository _userTasksRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public UserTasksService(IUserTasksRepository userTasksRepository, ISubscriptionsRepository subscriptionsRepository)
        {
            _userTasksRepository = userTasksRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<ApiResponse<IEnumerable<TaskItemDto>>> GetMyTasksAsync(Guid userId)
        {
            var subscription = await _subscriptionsRepository.GetByUserIdAsync(userId);
            var subscriptionLevel = subscription?.SubscriptionType ?? SubscriptionType.Basic;

            var tasks = await _userTasksRepository.GetTasksBySubscriptionLevelAsync(subscriptionLevel);

            var taskDtos = tasks.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                RequiredSubscriptionLevel = t.RequiredSubscriptionLevel
            }).ToList();

            return ApiResponse<IEnumerable<TaskItemDto>>.SuccessResponse(taskDtos, "Tasks retrieved successfully.");
        }

        public async Task<ApiResponse<TaskItemDto>> UpdateTaskStatusAsync(Guid userId, Guid taskId, UpdateTaskStatusDto dto)
        {
            var subscription = await _subscriptionsRepository.GetByUserIdAsync(userId);
            var subscriptionLevel = subscription?.SubscriptionType ?? SubscriptionType.Basic;

            var task = await _userTasksRepository.GetTaskByIdAsync(taskId);

            if (task == null)
            {
                return ApiResponse<TaskItemDto>.FailureResponse("Task not found.");
            }

            if (task.RequiredSubscriptionLevel > subscriptionLevel)
            {
                return ApiResponse<TaskItemDto>.FailureResponse("You do not have the required subscription level to access this task.");
            }

            task.Status = dto.Status;
            await _userTasksRepository.UpdateTaskStatusAsync(task);

            var taskDto = new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                RequiredSubscriptionLevel = task.RequiredSubscriptionLevel
            };

            return ApiResponse<TaskItemDto>.SuccessResponse(taskDto, "Task status updated successfully.");
        }
    }
}
