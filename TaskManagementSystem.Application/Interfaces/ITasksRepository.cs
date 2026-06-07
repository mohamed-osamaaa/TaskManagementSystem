using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface ITasksRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
    }
}
