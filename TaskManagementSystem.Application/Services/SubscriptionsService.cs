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
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IUsersRepository _usersRepository;

        public SubscriptionsService(ISubscriptionsRepository subscriptionsRepository, IUsersRepository usersRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _usersRepository = usersRepository;
        }

        public async Task<ApiResponse<SubscriptionDto>> AssignSubscriptionAsync(AssignSubscriptionDto dto)
        {
            var user = await _usersRepository.GetByIdAsync(dto.UserId);
            if (user == null)
            {
                return ApiResponse<SubscriptionDto>.FailureResponse("User not found.");
            }

            var existingSubscription = await _subscriptionsRepository.GetByUserIdAsync(dto.UserId);
            if (existingSubscription != null)
            {
                return ApiResponse<SubscriptionDto>.FailureResponse("User already has a subscription. Use update instead.");
            }

            var subscription = new UserSubscription
            {
                UserId = dto.UserId,
                SubscriptionType = dto.SubscriptionType
            };

            await _subscriptionsRepository.CreateAsync(subscription);

            var responseDto = new SubscriptionDto
            {
                Id = subscription.Id,
                UserId = subscription.UserId,
                UserEmail = user.Email ?? string.Empty,
                UserFullName = user.FullName,
                SubscriptionType = subscription.SubscriptionType
            };

            return ApiResponse<SubscriptionDto>.SuccessResponse(responseDto, "Subscription assigned successfully.");
        }

        public async Task<ApiResponse<SubscriptionDto>> UpdateSubscriptionAsync(Guid userId, UpdateSubscriptionDto dto)
        {
            var subscription = await _subscriptionsRepository.GetByUserIdAsync(userId);
            if (subscription == null)
            {
                return ApiResponse<SubscriptionDto>.FailureResponse("Subscription not found for this user.");
            }

            subscription.SubscriptionType = dto.SubscriptionType;
            await _subscriptionsRepository.UpdateAsync(subscription);

            var user = await _usersRepository.GetByIdAsync(userId);

            var responseDto = new SubscriptionDto
            {
                Id = subscription.Id,
                UserId = subscription.UserId,
                UserEmail = user?.Email ?? string.Empty,
                UserFullName = user?.FullName ?? string.Empty,
                SubscriptionType = subscription.SubscriptionType
            };

            return ApiResponse<SubscriptionDto>.SuccessResponse(responseDto, "Subscription updated successfully.");
        }

        public async Task<ApiResponse<IEnumerable<SubscriptionDto>>> GetAllSubscriptionsAsync()
        {
            var subscriptions = await _subscriptionsRepository.GetAllAsync();
            var subscriptionDtos = new List<SubscriptionDto>();

            foreach (var sub in subscriptions)
            {
                var user = await _usersRepository.GetByIdAsync(sub.UserId);
                subscriptionDtos.Add(new SubscriptionDto
                {
                    Id = sub.Id,
                    UserId = sub.UserId,
                    UserEmail = user?.Email ?? string.Empty,
                    UserFullName = user?.FullName ?? string.Empty,
                    SubscriptionType = sub.SubscriptionType
                });
            }

            return ApiResponse<IEnumerable<SubscriptionDto>>.SuccessResponse(subscriptionDtos, "Subscriptions retrieved successfully.");
        }

        public async Task<ApiResponse<SubscriptionDto>> GetSubscriptionByUserIdAsync(Guid userId)
        {
            var subscription = await _subscriptionsRepository.GetByUserIdAsync(userId);
            if (subscription == null)
            {
                return ApiResponse<SubscriptionDto>.FailureResponse("Subscription not found.");
            }

            var user = await _usersRepository.GetByIdAsync(userId);

            var responseDto = new SubscriptionDto
            {
                Id = subscription.Id,
                UserId = subscription.UserId,
                UserEmail = user?.Email ?? string.Empty,
                UserFullName = user?.FullName ?? string.Empty,
                SubscriptionType = subscription.SubscriptionType
            };

            return ApiResponse<SubscriptionDto>.SuccessResponse(responseDto, "Subscription retrieved successfully.");
        }
    }
}
