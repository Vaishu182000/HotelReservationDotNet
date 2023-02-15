using FluentValidation;
using HotelBooking.Data.ViewModels;
using HotelBooking.Services;

namespace HotelBooking.Data.Validators;

public class RoomValidator : AbstractValidator<RoomVM>
{
    public RoomValidator()
    {
        RuleFor(x => x.roomCapacity).NotEmpty().WithMessage("Room Capacity Cannot be Empty");
        RuleFor(x => x.roomName).NotEmpty().WithMessage("Room Name Cannot be Empty");
        RuleFor(x => x.hotelId).NotEmpty().WithMessage("Hotel Name Cannot be Empty");
        RuleFor(x => x.roomRate).NotEmpty().WithMessage("Room Rate Cannot be Empty");
    }
}