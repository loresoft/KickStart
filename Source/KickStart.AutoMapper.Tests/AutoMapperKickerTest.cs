using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using KickStart.Logging;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.AutoMapper.Tests
{

    public class AutoMapperKickerTest
    {
        public AutoMapperKickerTest(ITestOutputHelper output)
        {
            var writer = new DelegateLogWriter(d => output.WriteLine(d.ToString()));
            Logger.RegisterWriter(writer);
        }

        [Fact]
        public void ConfigureBasic()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<UserProfile>()
                .UseAutoMapper()
            );

            var employee = new Employee
            {
                FirstName = "Test",
                LastName = "User",
                EmailAddress = "test.user@email.com",
                SysVersion = BitConverter.GetBytes(8)
            };

            var user = Mapper.Map<User>(employee);
            user.Should().NotBeNull();
            user.EmailAddress.Should().Be(employee.EmailAddress);
            user.SysVersion.Should().NotBeNull();
        }

        [Fact]
        public void ConfigureFull()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<UserProfile>()
                .UseAutoMapper(c => c
                    .Validate()
                )
            );

            var employee = new Employee
            {
                FirstName = "Test",
                LastName = "User",
                EmailAddress = "test.user@email.com",
                SysVersion = BitConverter.GetBytes(8)
            };

            var user = Mapper.Map<User>(employee);
            user.Should().NotBeNull();
            user.EmailAddress.Should().Be(employee.EmailAddress);

        }
    }
}
