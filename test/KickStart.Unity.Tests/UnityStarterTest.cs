using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using KickStart.Logging;
using KickStart.Services;
using Microsoft.Practices.Unity;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.Unity.Tests
{
    public class UnityStarterTest
    {
        public UnityStarterTest(ITestOutputHelper output)
        {
            var writer = new DelegateLogWriter(d => output.WriteLine(d.ToString()));
            Logger.RegisterWriter(writer);
        }

        [Fact]
        public void UseUnity()
        {
            Kick.Start(config => config
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

    }
}
