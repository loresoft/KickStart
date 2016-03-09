using System;
using AutoMapper;

namespace Test.Core
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