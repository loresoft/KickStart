using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using KickStart.Services;
using Microsoft.Practices.Unity;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.Unity.Tests
{
    public class UnityStarterTest
    {
        private readonly ITestOutputHelper _output;

        public UnityStarterTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void UseUnity()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserUnityRegistration>()
                .UseUnity()
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<UnityServiceProvider>();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

        [Fact]
        public void UseUnityInitialize()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserUnityRegistration>()
                .UseUnity(c => c
                    .Container(b => b.RegisterType<Employee>())
                )
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<UnityServiceProvider>();

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
                .IncludeAssemblyFor<UserUnityRegistration>()
                .UseUnity()
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<UnityServiceProvider>();


            var userService = Kick.ServiceProvider.GetService<IUserService>();
            userService.Should().NotBeNull();
            userService.Connection.Should().NotBeNull();
            userService.Connection.Should().BeOfType<SampleConnection>();
        }

    }
}
