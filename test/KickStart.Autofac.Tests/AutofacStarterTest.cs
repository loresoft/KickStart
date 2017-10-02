using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using FluentAssertions;
using KickStart.Services;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.Autofac.Tests
{
    public class AutofacStarterTest
    {
        private readonly ITestOutputHelper _output;

        public AutofacStarterTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void UseAutofac()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserModule>()
                .UseAutofac()
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<AutofacServiceProvider>();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

        [Fact]
        public void UseAutofacBuilder()
        {
            string defaultEmail = "test@email.com";

            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserModule>()
                .UseAutofac(c => c
                    .Initialize(b => b
                        .Register(x => new Employee { EmailAddress = defaultEmail }
                    ))
                )
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<AutofacServiceProvider>();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();

            var employee = Kick.ServiceProvider.GetService<Employee>();
            employee.Should().NotBeNull();
            employee.EmailAddress.Should().Be(defaultEmail);

        }


        [Fact]
        public void UseAutofacBuilderLogTo()
        {
            string defaultEmail = "test@email.com";

            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserModule>()
                .UseAutofac(c => c
                    .Initialize(b => b
                        .Register(x => new Employee { EmailAddress = defaultEmail }
                    ))
                )
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<AutofacServiceProvider>();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();

            var employee = Kick.ServiceProvider.GetService<Employee>();
            employee.Should().NotBeNull();
            employee.EmailAddress.Should().Be(defaultEmail);
        }


        [Fact]
        public void UseServiceInitialize()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserModule>()
                .IncludeAssemblyFor<UserServiceModule>()
                .UseAutofac()
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<AutofacServiceProvider>();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();


            var userService = Kick.ServiceProvider.GetService<IUserService>();
            userService.Should().NotBeNull();
            userService.Should().BeOfType<UserService>();
            userService.Connection.Should().NotBeNull();
            userService.Connection.Should().BeOfType<SampleConnection>();

            var vehicleService = Kick.ServiceProvider.GetService<IVehicle>();
            vehicleService.Should().NotBeNull();
            vehicleService.Should().BeOfType<DeliveryVehicle>();

            var minivanService = Kick.ServiceProvider.GetService<IMinivan>();
            minivanService.Should().NotBeNull();
            minivanService.Should().BeOfType<DeliveryVehicle>();

            var services = Kick.ServiceProvider.GetServices<IService>().ToList();
            services.Should().NotBeNull();
            services.Should().NotBeEmpty();
            services.Count.Should().Be(3);

        }
    }
}
