using System;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs
{
    public class TaskItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskManagementSystem.Domain.Enums.TaskStatus Status { get; set; }
        public SubscriptionType RequiredSubscriptionLevel { get; set; }
    }
}
