using KickStart.Services;

using Test.Core;

namespace KickStart.Tests.Services;

public class ServiceTypeMapperTest
{
    [Fact]
    public void AsImplementedInterfaces()
    {
        var types = new[] { typeof(Service1), typeof(Service2), typeof(Service3) };
        var mapper = new ServiceTypeMapper(types);
        mapper.Should().NotBeNull();

        mapper.ImplementedInterfaces();
        mapper.TypeMaps.Should().NotBeEmpty();
        mapper.TypeMaps.Count.Should().Be(3);

        var typeMap1 = mapper.TypeMaps[0];
        typeMap1.ImplementationType.Should().Be(typeof(Service1));

        var serviceTypes = typeMap1.ServiceTypes.ToList();
        serviceTypes.Count.Should().Be(1);

        var serviceType1 = serviceTypes[0];
        serviceType1.Should().Be(typeof(IService));
    }


    [Fact]
    public void AsImplementedInterfacesMultiple()
    {
        var types = new[] { typeof(DeliveryVehicle) };
        var mapper = new ServiceTypeMapper(types);
        mapper.Should().NotBeNull();

        mapper.ImplementedInterfaces();
        mapper.TypeMaps.Should().NotBeEmpty();
        mapper.TypeMaps.Count.Should().Be(1);

        var typeMap1 = mapper.TypeMaps[0];
        typeMap1.ImplementationType.Should().Be(typeof(DeliveryVehicle));

        var serviceTypes = typeMap1.ServiceTypes.ToList();
        serviceTypes.Count.Should().Be(5);
        serviceTypes.Should().Contain(new[] { typeof(IVehicle), typeof(ICar), typeof(ITruck), typeof(IVan), typeof(IMinivan) });
    }

    [Fact]
    public void AsTypes()
    {
        var types = new[] { typeof(DeliveryVehicle) };
        var mapper = new ServiceTypeMapper(types);
        mapper.Should().NotBeNull();

        mapper.Types(typeof(IVan), typeof(IMinivan));
        mapper.TypeMaps.Should().NotBeEmpty();
        mapper.TypeMaps.Count.Should().Be(1);

        var typeMap1 = mapper.TypeMaps[0];
        typeMap1.ImplementationType.Should().Be(typeof(DeliveryVehicle));

        var serviceTypes = typeMap1.ServiceTypes.ToList();
        serviceTypes.Count.Should().Be(2);
        serviceTypes.Should().Contain(new[] { typeof(IVan), typeof(IMinivan) });
    }


    [Fact]
    public void AsSelfImplementedInterfaces()
    {
        var types = new[] { typeof(DeliveryVehicle) };
        var mapper = new ServiceTypeMapper(types);
        mapper.Should().NotBeNull();

        mapper.Self().ImplementedInterfaces();

        mapper.TypeMaps.Should().NotBeEmpty();
        mapper.TypeMaps.Count.Should().Be(2);


        var typeMap1 = mapper.TypeMaps[0];
        typeMap1.ImplementationType.Should().Be(typeof(DeliveryVehicle));

        var serviceTypes1 = typeMap1.ServiceTypes.ToList();
        serviceTypes1.Count.Should().Be(1);
        serviceTypes1.Should().Contain(new[] { typeof(DeliveryVehicle) });


        var typeMap2 = mapper.TypeMaps[1];
        typeMap1.ImplementationType.Should().Be(typeof(DeliveryVehicle));

        var serviceTypes2 = typeMap2.ServiceTypes.ToList();
        serviceTypes2.Count.Should().Be(5);
        serviceTypes2.Should().Contain(new[] { typeof(IVehicle), typeof(ICar), typeof(ITruck), typeof(IVan), typeof(IMinivan) });
    }
}
