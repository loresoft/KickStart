using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Test.Core;
using Xunit;

namespace KickStart.Unity.Tests
{
    public class UnityStarterTest
    {
        [Fact]
        public void UseUnity()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<User>()
                .UseUnity()
                .LogLevel(TraceLevel.Verbose)
            );

            Kick.Container.Should().NotBeNull();
            Kick.Container.Should().BeOfType<UnityAdaptor>();
            Kick.Container.As<IUnityContainer>().Should().BeOfType<UnityContainer>();

            var repo = Kick.Container.Resolve<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

        [Fact]
        public void UseUnityInitialize()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<User>()
                .UseUnity(c => c
                    .Container(b => b.RegisterType<Employee>())
                )
                .LogLevel(TraceLevel.Verbose)
            );

            Kick.Container.Should().NotBeNull();
            Kick.Container.Should().BeOfType<UnityAdaptor>();
            Kick.Container.As<IUnityContainer>().Should().BeOfType<UnityContainer>();
            
            var repo = Kick.Container.Resolve<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();

            var employee = Kick.Container.Resolve<Employee>();
            employee.Should().NotBeNull();
        }

    }
}
