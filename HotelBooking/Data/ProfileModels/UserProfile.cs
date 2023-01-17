using AutoMapper;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Data.ProfileModels;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserVM>()
            .ForMember(
                dest => dest.userName,
                opt => opt.MapFrom(src => $"{src.userName}"))
            .ForMember(
                dest => dest.userEmail,
                opt => opt.MapFrom(src => $"{src.userEmail}"))
            .ForMember(
                dest => dest.phone,
                opt => opt.MapFrom(src => $"{src.phone}"))
            .ForMember(
                dest => dest.password,
                opt => opt.MapFrom(src => $"{src.password}"))
            .ReverseMap();
    }
}