using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;

        public AuthService(IAuthRepository authRepository, IJwtService jwtService)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
        }

        public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterDto request)
        {
            var userExists = await _authRepository.FindByEmailAsync(request.Email);
            if (userExists != null)
                return ApiResponse<AuthResponseDto>.FailureResponse("User already exists!");

            ApplicationUser user = new()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Email,
                FullName = request.FullName
            };

            var result = await _authRepository.CreateUserAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.ToList();
                return ApiResponse<AuthResponseDto>.FailureResponse("User creation failed! Please check user details and try again.", errors);
            }

            // Assign "User" role by default
            await _authRepository.AddToRoleAsync(user, "User");

            var roles = await _authRepository.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user, roles);

            var responseDto = new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                Roles = roles,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            return ApiResponse<AuthResponseDto>.SuccessResponse(responseDto, "User created successfully!");
        }

        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto request)
        {
            var user = await _authRepository.FindByEmailAsync(request.Email);
            if (user != null && await _authRepository.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _authRepository.GetRolesAsync(user);
                var token = _jwtService.GenerateToken(user, userRoles);

                var responseDto = new AuthResponseDto
                {
                    Token = token,
                    Email = user.Email!,
                    FullName = user.FullName,
                    Roles = userRoles,
                    ExpiresAt = DateTime.UtcNow.AddDays(7)
                };

                return ApiResponse<AuthResponseDto>.SuccessResponse(responseDto, "Login successful.");
            }

            return ApiResponse<AuthResponseDto>.FailureResponse("Invalid credentials.");
        }

    }
}
