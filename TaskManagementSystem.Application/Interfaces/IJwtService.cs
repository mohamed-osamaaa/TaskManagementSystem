using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user,
                             IList<string> roles);
    }
}
