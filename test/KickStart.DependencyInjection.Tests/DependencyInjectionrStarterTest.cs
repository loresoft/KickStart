using Microsoft.Extensions.DependencyInjection;

using Test.Core;

namespace KickStart.DependencyInjection.Tests
{
    public class DependencyInjectionStarterTest
    {
        private readonly ITestOutputHelper _output;

        public DependencyInjectionStarterTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void UseDependencyInjection()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserDependencyInjectionRegistration>()
                .UseDependencyInjection()
            );

            Kick.ServiceProvider.Should().NotBeNull();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

        [Fact]
        public void UseDependencyInjectionInitialize()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<UserDependencyInjectionRegistration>()
                .UseDependencyInjection(c => c
                    .Initialize(b => b.AddTransient<Employee>())
                )
            );

            Kick.ServiceProvider.Should().NotBeNull();

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
                .IncludeAssemblyFor<UserDependencyInjectionRegistration>()
                .IncludeAssemblyFor<UserServiceModule>()
                .UseDependencyInjection()
            );

            Kick.ServiceProvider.Should().NotBeNull();

            var userRepository = Kick.ServiceProvider.GetService<IUserRepository>();
            userRepository.Should().NotBeNull();
            userRepository.Should().BeOfType<UserRepository>();

            var employeeRepository = Kick.ServiceProvider.GetService<IRepository<Employee>>();
            employeeRepository.Should().NotBeNull();
            employeeRepository.Should().BeOfType<EmployeeRepository>();

            var userService = Kick.ServiceProvider.GetService<IUserService>();
            userService.Should().NotBeNull();
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
