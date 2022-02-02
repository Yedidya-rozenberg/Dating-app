using System.Runtime.InteropServices;
using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;
using API.Extantions;

namespace API.helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser,MemberDto >()
            .ForMember(
                dest => dest.PhotoUrl,
                opt => opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).URL)
            )
            .ForMember(
                dest=>dest.Age,
                opt => opt.MapFrom(src=>src.BirtheDay.CalculateAge())
            );
            CreateMap<Photo,PhotoDto >();

        }
    }
}