using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Infrastructure.Persistence;

namespace TaskManagementSystem.Infrastructure.Repositories
{
    public class UserTasksRepository : IUserTasksRepository
    {
        private readonly ApplicationDbContext _context;

        public UserTasksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksBySubscriptionLevelAsync(SubscriptionType level)
        {
            return await _context.Tasks
                .Where(t => t.RequiredSubscriptionLevel <= level)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(Guid taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }

        public async Task UpdateTaskStatusAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
