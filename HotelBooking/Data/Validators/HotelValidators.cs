using System.Text.RegularExpressions;
using FluentValidation;
using HotelBooking.Data.ViewModels;
using HotelBooking.Services;

namespace HotelBooking.Data.Validators;

public class HotelValidators : AbstractValidator<HotelVM>
{
    public HotelValidators()
    {
        RuleFor(x => x.locationId).NotEmpty().WithMessage("Location Should not be Empty");
        RuleFor(x => x.hotelName).NotEmpty().WithMessage("Hotel Name cannot be Empty");
        RuleFor(x => x.noOfRooms).NotEmpty().WithMessage("No of Rooms Field cannot be Empty");
    }
}