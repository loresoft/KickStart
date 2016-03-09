using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using KickStart.Logging;
using SimpleInjector;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.SimpleInjector.Tests
{
    public class SimpleInjectorStarterTest
    {
        public SimpleInjectorStarterTest(ITestOutputHelper output)
        {
            var writer = new DelegateLogWriter(d => output.WriteLine(d.ToString()));
            Logger.RegisterWriter(writer);
        }

        [Fact]
        public void UseSimpleInjector()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<User>()
                .UseSimpleInjector()
            );

            Kick.Container.Should().NotBeNull();
            Kick.Container.Should().BeOfType<SimpleInjectorAdaptor>();
            Kick.Container.As<Container>().Should().BeOfType<Container>();

            var repo = Kick.Container.Resolve<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

        [Fact]
        public void UseSimpleInjectorInitialize()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<User>()
                .UseSimpleInjector(c => c
                    .Container(b => b.Register<Employee>())
                )
            );

            Kick.Container.Should().NotBeNull();
            Kick.Container.Should().BeOfType<SimpleInjectorAdaptor>();
            Kick.Container.As<Container>().Should().BeOfType<Container>();

            var repo = Kick.Container.Resolve<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();

            var employee = Kick.Container.Resolve<Employee>();
            employee.Should().NotBeNull();
        }

    }
}
