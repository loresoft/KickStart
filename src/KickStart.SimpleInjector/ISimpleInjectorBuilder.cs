using System;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    public interface ISimpleInjectorBuilder
    {
        ISimpleInjectorBuilder Container(Action<Container> initializer);
    }
}