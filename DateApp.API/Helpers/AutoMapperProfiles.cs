namespace DateApp.API.Helpers
 {
    using AutoMapper;
    using DateApp.API.Models.Dtos;
    using DateApp.API.Models;

    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles () {
            CreateMap<User, UsersForListDto> ();
            CreateMap<User, UserDetailsDto> ();
            CreateMap<Photo,PhotoForDetailsDto>();
        }
    }
}