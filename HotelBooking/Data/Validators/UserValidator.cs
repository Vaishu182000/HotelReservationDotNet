using System.Text.RegularExpressions;
using FluentValidation;
using HotelBooking.Data.ViewModels;
using HotelBooking.Services;

namespace HotelBooking.Data.Validators;

public class UserValidator : AbstractValidator<UserVM>
{
    public UserValidator()
    {
        RuleFor(x => x.userName).NotEmpty().WithMessage("Should Not be empty");
        RuleFor(x => x.userEmail).EmailAddress().WithMessage("Must be Email");
        RuleFor(x => x.phone).Length(10).WithMessage("Phone Number must be 10 digits");
        RuleFor(x => x.password).NotEmpty().WithMessage("Password Field cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
    }
}