using FluentValidation;
using InfinityCQRS.App.Commands.User;
using Microsoft.AspNetCore.Http;

namespace InfinityCQRS.App.Validators.Users
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator(IHttpContextAccessor context)
        {
            string invalid = "";

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage("{PropertyName} can not be null.")
                .NotEmpty()
                    .WithMessage("{PropertyName} can not be empty.");
        }
    }
}
