using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using FluentAssertions;
using KickStart.Logging;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.Autofac.Tests
{
    public class AutofacStarterTest
    {
        public AutofacStarterTest(ITestOutputHelper output)
        {
            var writer = new DelegateLogWriter(d => output.WriteLine(d.ToString()));
            Logger.RegisterWriter(writer);
        }

        [Fact]
        public void UseAutofac()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<UserModule>()
                .UseAutofac()
            );

            Kick.Container.Should().NotBeNull();
            Kick.Container.Should().BeOfType<AutofacAdaptor>();
            Kick.Container.As<IContainer>().Should().BeOfType<Container>();

            var repo = Kick.Container.Resolve<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

        [Fact]
        public void UseAutofacBuilder()
        {
            string defaultEmail = "test@email.com";

            Kick.Start(config => config
                .IncludeAssemblyFor<UserModule>()
                .UseAutofac(c => c
                    .Builder(b => b
                        .Register(x => new Employee { EmailAddress = defaultEmail }
                    ))
                )
            );

            Kick.Container.Should().NotBeNull();
            Kick.Container.Should().BeOfType<AutofacAdaptor>();
            Kick.Container.As<IContainer>().Should().BeOfType<Container>();
            
            var repo = Kick.Container.Resolve<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();

            var employee = Kick.Container.Resolve<Employee>();
            employee.Should().NotBeNull();
            employee.EmailAddress.Should().Be(defaultEmail);

        }


        [Fact]
        public void UseAutofacBuilderLogTo()
        {
            string defaultEmail = "test@email.com";
            var _logs = new List<LogData>();

            Kick.Start(config => config
                .IncludeAssemblyFor<UserModule>()
                .UseAutofac(c => c
                    .Builder(b => b
                        .Register(x => new Employee { EmailAddress = defaultEmail }
                    ))
                )
                .LogTo(_logs.Add)
            );

            Kick.Container.Should().NotBeNull();
            Kick.Container.Should().BeOfType<AutofacAdaptor>();
            Kick.Container.As<IContainer>().Should().BeOfType<Container>();
            
            var repo = Kick.Container.Resolve<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();

            var employee = Kick.Container.Resolve<Employee>();
            employee.Should().NotBeNull();
            employee.EmailAddress.Should().Be(defaultEmail);
        }
    }
}
