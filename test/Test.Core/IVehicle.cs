using System;

namespace Test.Core
{
    public interface IVehicle { }

    public interface ICar : IVehicle { }

    public interface ITruck : IVehicle { }

    public interface IVan : IVehicle { }

    public interface IMinivan : IVan { }

    public class DeliveryVehicle : IMinivan, ICar, ITruck { }

}