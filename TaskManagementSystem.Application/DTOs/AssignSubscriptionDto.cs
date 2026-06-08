using System;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs
{
    public class AssignSubscriptionDto
    {
        public Guid UserId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
    }
}
