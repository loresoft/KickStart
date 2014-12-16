using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Ninject;
using Test.Core;
using Xunit;

namespace KickStart.Ninject.Tests
{
    public class NinjectStarterTest
    {
        [Fact]
        public void UseNinject()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<User>()
                .UseNinject()
            );

            Kick.Container.Should().NotBeNull();
            Kick.Container.Should().BeOfType<NinjectAdaptor>();
            Kick.Container.As<IKernel>().Should().BeOfType<StandardKernel>();

            var repo = Kick.Container.Resolve<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

    }
}
