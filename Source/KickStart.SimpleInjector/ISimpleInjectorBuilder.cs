using System;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    public interface ISimpleInjectorBuilder
    {
        ISimpleInjectorBuilder Initialize(Action<Container> initializer);
    }
}