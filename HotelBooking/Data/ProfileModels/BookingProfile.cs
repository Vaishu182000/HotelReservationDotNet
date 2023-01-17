using AutoMapper;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Data.ProfileModels;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking, BookingVM>()
            .ForMember(dest => dest.noOfPersons,
                opt => opt.MapFrom(src => $"{src.noOfPersons}"))
            .ForMember(dest => dest.checkInTime,
                opt => opt.MapFrom(src => $"{src.checkInTime}"))
            .ForMember(dest => dest.checkOutTime,
                opt => opt.MapFrom(src => $"{src.checkOutTime}"))
            .ForMember(dest => dest.userId,
                opt => opt.MapFrom(src => $"{src.UserId}"))
            .ForMember(dest => dest.paid,
                opt => opt.MapFrom(src => $"{src.paid}"))
            .ForMember(dest => dest.roomId,
                opt => opt.MapFrom(src => $"{src.RoomId}"))
            .ReverseMap();
    }
}