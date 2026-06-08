using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Interfaces;

namespace TaskManagementSystem.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _usersRepository.GetAllAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _usersRepository.GetRolesAsync(user);
                userDtos.Add(new UserDto
                {
                    Id = user.Id.ToString(),
                    Email = user.Email ?? string.Empty,
                    FullName = user.FullName,
                    Roles = roles
                });
            }

            return ApiResponse<IEnumerable<UserDto>>.SuccessResponse(userDtos, "Users retrieved successfully.");
        }

        public async Task<ApiResponse<UserDto>> GetUserByIdAsync(Guid id)
        {
            var user = await _usersRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<UserDto>.FailureResponse("User not found.");
            }

            var roles = await _usersRepository.GetRolesAsync(user);
            var userDto = new UserDto
            {
                Id = user.Id.ToString(),
                Email = user.Email ?? string.Empty,
                FullName = user.FullName,
                Roles = roles
            };

            return ApiResponse<UserDto>.SuccessResponse(userDto, "User retrieved successfully.");
        }
    }
}
