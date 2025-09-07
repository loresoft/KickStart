using System.Reflection;

using Test.Core;

namespace KickStart.Tests;

public class AssemblyResolverTest
{
    private readonly ITestOutputHelper _output;

    public AssemblyResolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

#if !(PORTABLE || NETSTANDARD1_3 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETCOREAPP1_0)
    [Fact]
    public void DefaultResolve()
    {

        var resolver = new AssemblyResolver(_output.WriteLine);
        resolver.Should().NotBeNull();

        var assemblies = resolver.Resolve().ToList();
        assemblies.Should().NotBeEmpty();
        assemblies.Should().Contain(a => a.FullName.StartsWith("System"));

    }

    [Fact]
    public void ExcludeSystem()
    {
        var resolver = new AssemblyResolver(_output.WriteLine);
        resolver.Should().NotBeNull();

        resolver.ExcludeName("System");

        var assemblies = resolver.Resolve().ToList();
        assemblies.Should().NotBeEmpty();
        assemblies.Should().NotContain(a => a.FullName.StartsWith("System"));
    }
#endif

    [Fact]
    public void SystemExplicit()
    {
        var resolver = new AssemblyResolver(_output.WriteLine);
        resolver.Should().NotBeNull();

        // add a few defaults
        resolver.IncludeAssemblyFor<Guid>();
        resolver.IncludeAssemblyFor<Uri>();
        resolver.IncludeAssemblyFor<Assembly>();
        resolver.IncludeAssemblyFor<ParallelQuery>();
        resolver.IncludeAssemblyFor<SampleWorker>();


        var assemblies = resolver.Resolve().ToList();
        assemblies.Should().NotBeEmpty();
        assemblies.Should().Contain(a => a.FullName.StartsWith("System"));
    }

    [Fact]
    public void ExcludeSystemExplicit()
    {
        var resolver = new AssemblyResolver(_output.WriteLine);
        resolver.Should().NotBeNull();

        // add a few defaults
        resolver.IncludeAssemblyFor<Guid>();
        resolver.IncludeAssemblyFor<Uri>();
        resolver.IncludeAssemblyFor<Assembly>();
        resolver.IncludeAssemblyFor<ParallelQuery>();
        resolver.IncludeAssemblyFor<SampleWorker>();

        resolver.ExcludeName("System");

        var assemblies = resolver.Resolve().ToList();
        assemblies.Should().NotBeEmpty();
        assemblies.Should().NotContain(a => a.FullName.StartsWith("System"));
    }


    [Fact]
    public void IncludeAssemblyForTestCore()
    {
        var resolver = new AssemblyResolver(_output.WriteLine);
        resolver.Should().NotBeNull();

        resolver.IncludeAssemblyFor<SampleWorker>();

        var assemblies = resolver.Resolve().ToList();
        assemblies.Should().NotBeEmpty();
        assemblies.Count.Should().Be(1);
        assemblies.Should().Contain(a => a == typeof(SampleWorker).GetTypeInfo().Assembly);
    }
}
