using Test.Core;

namespace KickStart.Tests;

public class AssemblyExtensionsTest
{
    [Fact]
    public void IsAssignableTo()
    {
        var result = typeof(IVehicle).IsAssignableTo(typeof(DeliveryVehicle));
        result.Should().BeFalse();


        result = typeof(DeliveryVehicle).IsAssignableTo(typeof(IVehicle));
        result.Should().BeTrue();
    }

}
