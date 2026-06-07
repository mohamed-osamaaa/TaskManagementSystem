using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Interfaces;

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
    }
}
