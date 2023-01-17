using FluentValidation;
using HotelBooking.Data.ViewModels;
using HotelBooking.Services;

namespace HotelBooking.Data.Validators;

public class RoomValidator  : AbstractValidator<RoomVM>
{
    private HotelService _hotelService;
    
    public RoomValidator(HotelService hotelService)
    {
        _hotelService = hotelService;
        
        RuleFor(x => x.roomCapacity).NotEmpty().WithMessage("Room Capacity Cannot be Empty");
        RuleFor(x => x.roomName).NotEmpty().WithMessage("Room Name Cannot be Empty");
        RuleFor(x => x.hotelId).NotEmpty().WithMessage("Hotel Name Cannot be Empty");
        RuleFor(x => x.roomRate).NotEmpty().WithMessage("Room Rate Cannot be Empty");
    }

    private bool doesHotelExistsInDB(string hotelName)
    {
        var _hotel = _hotelService.GetHotelByHotelName(hotelName);
        
        if (_hotel == null) return false;
        else return true;
    }
}