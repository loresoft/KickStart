using System;
using AutoMapper;
using Test.Core;

namespace KickStart.AutoMapper.Tests
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<User, Employee>();
            CreateMap<Employee, User>();
        }
    }
}