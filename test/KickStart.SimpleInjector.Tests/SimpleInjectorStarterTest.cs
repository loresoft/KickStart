using System;
using System.Collections.Generic;
using FluentAssertions;
using KickStart.Services;
using KickStart.StartupTask;
using SimpleInjector;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.SimpleInjector.Tests
{
    public class SimpleInjectorStarterTest
    {
        private readonly ITestOutputHelper _output;

        public SimpleInjectorStarterTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void UseSimpleInjector()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserSimpleInjectorRegistration>()
                .UseSimpleInjector()
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<Container>();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

        [Fact]
        public void UseSimpleInjectorInitialize()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserSimpleInjectorRegistration>()
                .UseSimpleInjector(c => c
                    .Container(b => b.Register<Employee>())
                )
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<Container>();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();

            var employee = Kick.ServiceProvider.GetService<Employee>();
            employee.Should().NotBeNull();

        }


        [Fact]
        public void UseServiceInitialize()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserSimpleInjectorRegistration>()
                .IncludeAssemblyFor<UserServiceModule>()
                .UseSimpleInjector()
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<Container>();


            var userService = Kick.ServiceProvider.GetService<IUserService>();
            userService.Should().NotBeNull();
            userService.Connection.Should().NotBeNull();
            userService.Connection.Should().BeOfType<SampleConnection>();
        }


        [Fact]
        public void InjectorStartTask()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserSimpleInjectorRegistration>()
                .IncludeAssemblyFor<UserServiceModule>()
                .UseSimpleInjector()
                .UseStartupTask()
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<Container>();


            var userService = Kick.ServiceProvider.GetService<IUserService>();
            userService.Should().NotBeNull();
            userService.Connection.Should().NotBeNull();
            userService.Connection.Should().BeOfType<SampleConnection>();
        }
    }
}
