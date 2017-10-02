using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentAssertions;
using KickStart.Services;
using Test.Core;
using Xunit;

namespace KickStart.Tests.Services
{
    public class ConcreteTypeFilterTest
    {
        [Fact]
        public void AssignableToVehicle()
        {
            var types = typeof(IVehicle).GetTypeInfo().Assembly.GetLoadableTypes().ToList();
            types.Should().NotBeEmpty();

            var filter = new ConcreteTypeFilter(types);
            filter.AssignableTo(typeof(IVehicle));

            var filteredTypes = filter.FilteredTypes.ToList();
            filteredTypes.Should().NotBeEmpty();
            filteredTypes.Count.Should().Be(1);
        }

        [Fact]
        public void AssignableToService()
        {
            var types = typeof(IService).GetTypeInfo().Assembly.GetLoadableTypes().ToList();
            types.Should().NotBeEmpty();

            var filter = new ConcreteTypeFilter(types);
            filter.AssignableTo(typeof(IService));

            var filteredTypes = filter.FilteredTypes.ToList();
            filteredTypes.Should().NotBeEmpty();
            filteredTypes.Count.Should().Be(3);
        }
    }
}
