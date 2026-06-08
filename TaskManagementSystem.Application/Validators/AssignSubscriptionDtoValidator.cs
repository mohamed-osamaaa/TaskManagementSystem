using FluentValidation;
using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Validators
{
    public class AssignSubscriptionDtoValidator : AbstractValidator<AssignSubscriptionDto>
    {
        public AssignSubscriptionDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.SubscriptionType)
                .IsInEnum().WithMessage("A valid subscription type is required.");
        }
    }
}
