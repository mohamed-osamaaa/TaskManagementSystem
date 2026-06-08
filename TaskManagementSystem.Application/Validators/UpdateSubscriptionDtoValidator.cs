using FluentValidation;
using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Validators
{
    public class UpdateSubscriptionDtoValidator : AbstractValidator<UpdateSubscriptionDto>
    {
        public UpdateSubscriptionDtoValidator()
        {
            RuleFor(x => x.SubscriptionType)
                .IsInEnum().WithMessage("A valid subscription type is required.");
        }
    }
}
