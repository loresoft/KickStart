using System;
using System.Collections.Generic;
using System.Linq;
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
                .UseSimpleInjector(s => s.Initialize(c => c.Options.AllowOverridingRegistrations = true))
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<Container>();


            var userService = Kick.ServiceProvider.GetService<IUserService>();
            userService.Should().NotBeNull();
            userService.Connection.Should().NotBeNull();
            userService.Connection.Should().BeOfType<SampleConnection>();

            var vehicleService = Kick.ServiceProvider.GetService<IVehicle>();
            vehicleService.Should().NotBeNull();
            vehicleService.Should().BeOfType<DeliveryVehicle>();

            var minivanService = Kick.ServiceProvider.GetService<IMinivan>();
            minivanService.Should().NotBeNull();
            minivanService.Should().BeOfType<DeliveryVehicle>();
        }


        [Fact]
        public void InjectorStartTask()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserSimpleInjectorRegistration>()
                .IncludeAssemblyFor<UserServiceModule>()
                .UseSimpleInjector(s => s.Initialize(c => c.Options.AllowOverridingRegistrations = true))
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
