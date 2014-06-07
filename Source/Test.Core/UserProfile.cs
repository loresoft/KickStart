using System;
using AutoMapper;

namespace Test.Core
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, Employee>();
            Mapper.CreateMap<Employee, User>();
        }
    }
}