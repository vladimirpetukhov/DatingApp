namespace DateApp.API.Helpers {
    using System.Linq;
    using AutoMapper;
    using DateApp.API.Models.Dtos;
    using DateApp.API.Models;

    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles () {
            CreateMap<User, UsersForListDto> ()
                .ForMember (dest => dest.PhotoUrl, opt =>
                    opt.MapFrom (src => src.Photos.FirstOrDefault (p => p.IsMain).Url))
                .ForMember (dest => dest.Age, opt =>
                    opt.MapFrom (src => src.DateOfBirth.CalculateAge ()));
            CreateMap<User, UserDetailsDto> ()
                .ForMember (dest => dest.PhotoUrl, opt =>
                    opt.MapFrom (src => src.Photos.FirstOrDefault (p => p.IsMain).Url))
                .ForMember (dest => dest.Age, opt =>
                    opt.MapFrom (src => src.DateOfBirth.CalculateAge ()));
            CreateMap<Photo, PhotoForDetailsDto> ();
        }
    }
}