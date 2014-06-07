using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Test.Core;
using Xunit;

namespace KickStart.Tests
{
    public class AssemblyResolverTest
    {

        [Fact]
        public void DefaultResolve()
        {

            var resolver = new AssemblyResolver();
            resolver.Should().NotBeNull();

            var assemblies = resolver.Resolve().ToList();
            assemblies.Should().NotBeEmpty();
            assemblies.Should().Contain(a => a.FullName.StartsWith("System"));

        }

        [Fact]
        public void ExcludeSystem()
        {
            var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            var resolver = new AssemblyResolver();
            resolver.Should().NotBeNull();

            resolver.ExcludeName("System");

            var assemblies = resolver.Resolve().ToList();
            assemblies.Should().NotBeEmpty();
            assemblies.Should().NotContain(a => a.FullName.StartsWith("System"));
        }

        [Fact]
        public void IncludeAssemblyForTestCore()
        {
            var resolver = new AssemblyResolver();
            resolver.Should().NotBeNull();

            resolver.IncludeAssemblyFor<SampleWorker>();

            var assemblies = resolver.Resolve().ToList();
            assemblies.Should().NotBeEmpty();
            assemblies.Count.Should().Be(1);
            assemblies.Should().Contain(a => a == typeof(SampleWorker).Assembly);
        }
    }
}
