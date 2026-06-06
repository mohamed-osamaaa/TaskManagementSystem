using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities;


public class TaskItem
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Enums.TaskStatus Status { get; set; }

    public SubscriptionType RequiredSubscriptionLevel { get; set; }
}