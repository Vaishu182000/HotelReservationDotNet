using FluentValidation;
using HotelBooking.Data.ViewModels;
using HotelBooking.Services;

namespace HotelBooking.Data.Validators;

public class BookingValidator : AbstractValidator<BookingVM>
{

    public BookingValidator()
    {
        RuleFor(x => x.noOfPersons).NotEmpty().WithMessage("No of Persons Field Cannot be Empty");
        RuleFor(x => x.paid).NotEmpty().WithMessage("Paid Field Cannot be Empty");
        RuleFor(x => x.checkInTime).NotEmpty()
            .WithMessage("Check In Date Cannot be Empty");
        RuleFor(x => x.checkOutTime).NotEmpty()
            .WithMessage("Check In Date Cannot be Empty");
        RuleFor(x => x.roomId).NotEmpty().WithMessage("Room Id cannot be Empty");
        RuleFor(x => x.hotelId).NotEmpty().WithMessage("Hotel Id cannot be Empty");
        RuleFor(x => x.userId).NotEmpty().WithMessage("User Email cannot be Empty");
    }
}