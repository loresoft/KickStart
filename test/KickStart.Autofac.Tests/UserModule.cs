using System;
using Autofac;
using Test.Core;

namespace KickStart.Autofac.Tests
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();

        }
    }
}