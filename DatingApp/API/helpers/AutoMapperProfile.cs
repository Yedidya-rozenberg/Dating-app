using System.Runtime.InteropServices;
using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;
using API.Extensions;

namespace API.helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser,MemberDto >()
            .ForMember(
                dest => dest.PhotoUrl,
                opt => opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url)
            )
            .ForMember(
                dest=>dest.Age,
                opt => opt.MapFrom(src=>src.DateOfBirth.CalculateAge())
            );
            CreateMap<Photo,PhotoDto >();

            CreateMap<MemberUpdateDto, AppUser>();

            CreateMap<RegisterDto,AppUser>().ForMember(dest=>dest.UserName,
            opt=>opt.MapFrom(src=>src.Username.ToLower()));

        }
    }
}