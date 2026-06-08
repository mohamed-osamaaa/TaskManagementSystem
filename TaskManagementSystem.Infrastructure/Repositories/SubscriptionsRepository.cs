using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Persistence;

namespace TaskManagementSystem.Infrastructure.Repositories
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserSubscription?> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserSubscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<IEnumerable<UserSubscription>> GetAllAsync()
        {
            return await _context.UserSubscriptions.ToListAsync();
        }

        public async Task CreateAsync(UserSubscription subscription)
        {
            await _context.UserSubscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserSubscription subscription)
        {
            _context.UserSubscriptions.Update(subscription);
            await _context.SaveChangesAsync();
        }
    }
}
