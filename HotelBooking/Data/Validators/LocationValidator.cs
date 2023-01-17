using FluentValidation;
using HotelBooking.Data.ViewModels;

namespace HotelBooking.Data.Validators;

public class LocationValidator : AbstractValidator<LocationVM>
{
    public LocationValidator()
    {
        RuleFor(x => x.location).NotEmpty().WithMessage("Location Should Not be Empty");
    }
}