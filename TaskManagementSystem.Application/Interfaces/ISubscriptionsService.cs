using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common;
using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface ISubscriptionsService
    {
        Task<ApiResponse<SubscriptionDto>> AssignSubscriptionAsync(AssignSubscriptionDto dto);
        Task<ApiResponse<SubscriptionDto>> UpdateSubscriptionAsync(Guid userId, UpdateSubscriptionDto dto);
        Task<ApiResponse<IEnumerable<SubscriptionDto>>> GetAllSubscriptionsAsync();
        Task<ApiResponse<SubscriptionDto>> GetSubscriptionByUserIdAsync(Guid userId);
    }
}
