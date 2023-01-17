using AutoMapper;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Data.ProfileModels;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomVM>()
            .ForMember(dest => dest.hotelId,
                opt => opt.MapFrom(src => $"{src.HotelId}"))
            .ForMember(dest => dest.roomName,
                opt => opt.MapFrom(src => $"{src.roomName}"))
            .ForMember(dest => dest.roomCapacity,
                opt => opt.MapFrom(src => $"{src.roomCapacity}"))
            .ForMember(dest => dest.roomRate,
                opt => opt.MapFrom(src => $"{src.roomRate}"))
            .ReverseMap();
    }
}