using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface ISubscriptionsRepository
    {
        Task<UserSubscription?> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<UserSubscription>> GetAllAsync();
        Task CreateAsync(UserSubscription subscription);
        Task UpdateAsync(UserSubscription subscription);
    }
}
