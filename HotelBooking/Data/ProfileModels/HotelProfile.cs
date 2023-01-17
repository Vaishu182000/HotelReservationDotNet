using AutoMapper;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Data.ProfileModels;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<HotelVM, Hotel>()
            .ForMember(dest => dest.LocationId,
                opt => opt.MapFrom(src => $"{src.locationId}"))
            .ForMember(dest => dest.hotelName,
                opt => opt.MapFrom(src => $"{src.hotelName}"))
            .ForMember(dest => dest.noOfRooms,
                opt => opt.MapFrom(src => $"{src.noOfRooms}"))
            .ReverseMap();
    }
}