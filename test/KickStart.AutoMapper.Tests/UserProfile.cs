using System;
using AutoMapper;
using Test.Core;

namespace KickStart.AutoMapper.Tests
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, Employee>();
            CreateMap<Employee, User>();
        }
    }
}