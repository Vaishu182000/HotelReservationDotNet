using AutoMapper;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Data.ProfileModels;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationVM>()
            .ForMember(
                dest => dest.location,
                opt => opt.MapFrom(src => $"{src.location}"))
            .ReverseMap();
    }
}