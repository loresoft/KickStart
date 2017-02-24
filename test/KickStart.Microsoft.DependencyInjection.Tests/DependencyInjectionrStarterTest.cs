using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.Microsoft.DependencyInjection.Tests
{
    public class DependencyInjectionStarterTest
    {
        private readonly ITestOutputHelper _output;

        public DependencyInjectionStarterTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void UseSimpleInjector()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserDependencyInjectionRegistration>()
                .UseDependencyInjection()
            );

            Kick.ServiceProvider.Should().NotBeNull();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

        [Fact]
        public void UseSimpleInjectorInitialize()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserDependencyInjectionRegistration>()
                .UseDependencyInjection(c => c
                    .Services(b => b.AddTransient<Employee>())
                )
            );

            Kick.ServiceProvider.Should().NotBeNull();

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
                .IncludeAssemblyFor<UserDependencyInjectionRegistration>()
                .IncludeAssemblyFor<UserServiceModule>()
                .UseDependencyInjection()
            );

            Kick.ServiceProvider.Should().NotBeNull();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();

            var userService = Kick.ServiceProvider.GetService<IUserService>();
            userService.Should().NotBeNull();
            userService.Connection.Should().NotBeNull();
            userService.Connection.Should().BeOfType<SampleConnection>();
        }


    }
}
