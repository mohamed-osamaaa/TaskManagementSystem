using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;

        public TasksService(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public async Task<ApiResponse<IEnumerable<TaskItemDto>>> GetAllTasksAsync()
        {
            var tasks = await _tasksRepository.GetAllAsync();
            
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

        public async Task<ApiResponse<TaskItemDto>> GetTaskByIdAsync(Guid id)
        {
            var task = await _tasksRepository.GetByIdAsync(id);

            if (task == null)
            {
                return ApiResponse<TaskItemDto>.FailureResponse("Task not found.");
            }

            var taskDto = new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                RequiredSubscriptionLevel = task.RequiredSubscriptionLevel
            };

            return ApiResponse<TaskItemDto>.SuccessResponse(taskDto, "Task retrieved successfully.");
        }

        public async Task<ApiResponse<TaskItemDto>> CreateTaskAsync(TaskItemDto taskDto)
        {
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status,
                RequiredSubscriptionLevel = taskDto.RequiredSubscriptionLevel
            };

            await _tasksRepository.CreateTaskAsync(task);

            return ApiResponse<TaskItemDto>.SuccessResponse(taskDto, "Task created successfully.");
        }

        public async Task<ApiResponse<TaskItemDto>> UpdateTaskAsync(Guid id, TaskItemDto taskDto)
        {
            var task = await _tasksRepository.GetByIdAsync(id);

            if (task == null)
            {
                return ApiResponse<TaskItemDto>.FailureResponse("Task not found.");
            }

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.Status = taskDto.Status;
            task.RequiredSubscriptionLevel = taskDto.RequiredSubscriptionLevel;

            await _tasksRepository.UpdateTaskAsync(task);

            return ApiResponse<TaskItemDto>.SuccessResponse(taskDto, "Task updated successfully.");
        }

        public async Task<ApiResponse<TaskItemDto>> DeleteTaskAsync(Guid id)
        {
            var task = await _tasksRepository.GetByIdAsync(id);

            if (task == null)
            {
                return ApiResponse<TaskItemDto>.FailureResponse("Task not found.");
            }

            await _tasksRepository.DeleteTaskAsync(task);

            return ApiResponse<TaskItemDto>.SuccessResponse(null!, "Task deleted successfully.");
        }
    }
}
