using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using KickStart.Services;
using Ninject;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.Ninject.Tests
{
    public class NinjectStarterTest
    {
        private readonly ITestOutputHelper _output;

        public NinjectStarterTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void UseNinject()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserNinjectModule>()
                .UseNinject()
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<StandardKernel>();
            Kick.ServiceProvider.As<IKernel>().Should().BeOfType<StandardKernel>();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

    }
}
