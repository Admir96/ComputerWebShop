using API.DTOs;
using API.Entities;
using AutoMapper;
using System;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<RegisterDTO, User>();
        }


    }
}
