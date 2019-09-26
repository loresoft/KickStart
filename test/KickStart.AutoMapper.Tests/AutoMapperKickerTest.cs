using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.AutoMapper.Tests
{

    public class AutoMapperKickerTest
    {
        private readonly ITestOutputHelper _output;

        public AutoMapperKickerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ConfigureBasic()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserProfile>()
                .UseAutoMapper()
            );

            Kick.Data.TryGetValue(AutoMapperStarter.AutoMapperConfiguration, out var value);
            
            var configuration = value as IConfigurationProvider;
            configuration.Should().NotBeNull();

            var employee = new Employee
            {
                FirstName = "Test",
                LastName = "User",
                EmailAddress = "test.user@email.com",
                SysVersion = BitConverter.GetBytes(8)
            };

            var mapper = configuration.CreateMapper();
            mapper.Should().NotBeNull();

            var user = mapper.Map<User>(employee);
            user.Should().NotBeNull();
            user.EmailAddress.Should().Be(employee.EmailAddress);
            user.SysVersion.Should().NotBeNull();
        }

        [Fact]
        public void ConfigureFull()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserProfile>()
                .UseAutoMapper(c => c
                    .Validate()
                )
            );

            Kick.Data.TryGetValue(AutoMapperStarter.AutoMapperConfiguration, out var value);

            var configuration = value as IConfigurationProvider;
            configuration.Should().NotBeNull();

            var employee = new Employee
            {
                FirstName = "Test",
                LastName = "User",
                EmailAddress = "test.user@email.com",
                SysVersion = BitConverter.GetBytes(8)
            };

            var mapper = configuration.CreateMapper();
            mapper.Should().NotBeNull();

            var user = mapper.Map<User>(employee);
            user.Should().NotBeNull();
            user.EmailAddress.Should().Be(employee.EmailAddress);

        }
    }
}
