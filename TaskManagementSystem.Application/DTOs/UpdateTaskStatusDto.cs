using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs
{
    public class UpdateTaskStatusDto
    {
        public TaskManagementSystem.Domain.Enums.TaskStatus Status { get; set; }
    }
}
